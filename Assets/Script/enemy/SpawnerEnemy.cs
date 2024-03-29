﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EventTransform : UnityEvent<Transform> { }

[Serializable]
public class EnemyBehaviorCount 
{
    public int Count;
    public EnemyAIBase Type;
    public void MinusCount()=>Count--;

}

public class SpawnerEnemy : MonoBehaviour
{

    [SerializeField] List<EnemyBehaviorCount> _avalibBehavior = new List<EnemyBehaviorCount>();     // список доспупных поведений
    [SerializeField] int           _maxOneTimeEnemy = 10;
                                            //  x - x    z  -  z 
    [SerializeField] float[,]      _siteCoordinate = 
                                               { { 15f, 16f, 10f,-10.5f  },      //Up
                                                 {-15f,-16f, 10f,-10.5f  },      //Down
                                                 {-15f, 16f, 10f, 10.5f  },      //Left
                                                 {-15f, 16f,-10f,-10.5f  } };    //Right

    #region counters
    [SerializeField]
    private int                  _enemyCountNow = 0;
    private int                  _allAvalibleCount;                       // couint enemy on lvl
    private List<Transform>      _enemyOnGame = new List<Transform>();

    public int             GetCountOnGame()    => _enemyCountNow;
    public List<Transform> GetTransformAlive() => _enemyOnGame;
    public int             GetLeft()           => _allAvalibleCount;
    #endregion

    private Transform      _gotoEnemy;
    public  EventTransform OnSpawnEnemy = new EventTransform();
    public static SpawnerEnemy Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance obj over 1");
        _gotoEnemy = transform;

        GlobalEventsManager.OnEnemyKill.AddListener(MinusEnemy);
    }

    private void Start()
    {
        foreach (EnemyBehaviorCount temp in _avalibBehavior) 
        {
            _allAvalibleCount += temp.Count;
        }
        InvokeRepeating(nameof(ChoiseSite), 0f, 1f);
    }

    public void MinusEnemy(Transform enemy)
    {
        _enemyCountNow--;
        _enemyOnGame.Remove(enemy);
    }

    public void AddNewBehaivor(EnemyAIBase enemyAI, int count) 
    {
        EnemyBehaviorCount temp = new EnemyBehaviorCount { Type = enemyAI, Count = count };
        _avalibBehavior.Add(temp);
    }

    private void ChoiseSite() 
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

    private void Spawn(float x,float z) 
    {
        Transform Enemy = ObjPool.Instance.SpawnObj(TypeObj.Enemy,new Vector3(x,0,z));
        if (Enemy != null)
        {
            _enemyOnGame.Add(Enemy);
            Enemy.gameObject.AddComponent(ChoiseBehaivor().GetType());
            EnemyAIBase tempAi = Enemy.GetComponent<EnemyAIBase>();
            tempAi.GoTo = _gotoEnemy;
            OnSpawnEnemy?.Invoke(Enemy);
        }
    }

    private EnemyAIBase ChoiseBehaivor()
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
}
