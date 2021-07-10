using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

[Serializable]
class EnemyBehaviorCount 
{
    public int Count;
    public EnemyAIBase Type;
    public void MinusCount()=>Count--;

}

public class SpawnerEnemy : MonoBehaviour
{

    [SerializeField] List<EnemyBehaviorCount> _avalibBehavior = new List<EnemyBehaviorCount>();
    [SerializeField] int           _maxOneTimeEnemy = 10;
    [SerializeField] int           _enemyCountNow   = 0;
                                            //  x - x    z  -  z 
    [SerializeField] float[,]      _siteCoordinate = 
                                               { { 15f, 16f, 10f,-10.5f  },      //Up
                                                 {-15f,-16f, 10f,-10.5f  },      //Down
                                                 {-15f, 16f, 10f, 10.5f  },      //Left
                                                 {-15f, 16f,-10f,-10.5f  } };    //Right
    int       _allAvalibleCount;
    Transform _gotoEnemy;
    public static SpawnerEnemy Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance obj over 1");
        _gotoEnemy = transform;
    }

    void Start()
    {
        foreach (EnemyBehaviorCount temp in _avalibBehavior) 
        {
            _allAvalibleCount += temp.Count;
        }
        InvokeRepeating(nameof(ChoiseSite), 0f, 1f);
    }

    public void MinusEnemy() => _enemyCountNow--;

    public void AddNewBehaivor(EnemyAIBase enemyAI, int count) 
    {
        EnemyBehaviorCount temp = new EnemyBehaviorCount { Type = enemyAI, Count = count };
        _avalibBehavior.Add(temp);
    }

    void ChoiseSite() 
    {
        if (_allAvalibleCount > 0)
        {
            if (_enemyCountNow <= _maxOneTimeEnemy)
            {
                _enemyCountNow++;
                int i = Random.Range(0, 4);
                Spawn(Random.Range(_siteCoordinate[i, 0], _siteCoordinate[i, 1]), Random.Range(_siteCoordinate[i, 2], _siteCoordinate[i, 3]));
            }
        }
        else 
        {
            CancelInvoke(nameof(ChoiseSite));
        }
    }

    void Spawn(float x,float z) 
    {
        Transform temp = ObjPool.Instance.SpawnObj(TypeObj.Enemy,new Vector3(x,0,z));
        if (temp != null)
        {
            temp.gameObject.AddComponent(ChoiseBehaivor().GetType());
            EnemyAIBase tempAi = temp.GetComponent<EnemyAIBase>();
            tempAi.GoTo = _gotoEnemy;
        }
    }

    EnemyAIBase ChoiseBehaivor()
    {
        _allAvalibleCount--;
        int chBeh = Random.Range(0, _avalibBehavior.Count);
        EnemyAIBase temp = _avalibBehavior[chBeh].Type;
        _avalibBehavior[chBeh].MinusCount();
        if (_avalibBehavior[chBeh].Count == 0)
        {
            _avalibBehavior.RemoveAt(chBeh);
        }
        return temp;
    }

    IEnumerator SetAI(Transform enemy,EnemyAIBase behaivor,Transform Goto) 
    {
        //yield return null;
        //Destroy(enemy.GetComponent<EnemyAIBase>());
        //yield return null;
        enemy.gameObject.AddComponent(behaivor.GetType());
        EnemyAIBase tempAi = enemy.GetComponent<EnemyAIBase>();
        tempAi.GoTo = Goto;
        yield return null;

    }
}
