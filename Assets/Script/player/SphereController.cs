using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    //отвечает за хп и макс хп сферы, отключает или включает slowMotion у врагов, а так же отрисовывает свое хп на внутриигровом интерфейсе Road

    float _HpSphere = 5;
    float _HpCurretSphere;
    bool  _timerActive = false;
    public SpriteRenderer  MaskRoad;
    List<EnemyObj>         _enemyList = new List<EnemyObj>();   // те кто подошел достаточно близко

    public static SphereController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance obj over 1");
    }

    private void Start()
    {
        _HpCurretSphere = _HpSphere;
        HpLine.init(MaskRoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy") 
        {
            EnemyObj temp = other.GetComponent<EnemyObj>();
            if (!_enemyList.Contains(temp) && temp!=null)
            {
                AddEnemy(temp);
                temp.Attack();                      // Start attack anim
                temp.InSphere();
                temp.SlowTimeEnable(_timerActive); 
            }
        }
    }

    private void FixedUpdate()
    {
        if (_timerActive && _enemyList.Count >= 1)
        {
            // тут тратим пока врагов не останется или пока щит не кончится 
            if (_enemyList.Count == 0)
                _timerActive = false;

            _HpCurretSphere -= 0.02f;

            HpLine.DrawCallRoadHp(_HpCurretSphere, _HpSphere);
            if (_HpCurretSphere <= 0)
            {
                _timerActive = false;
                ActicateSlowM(false);
            }
        }
        else if (!_timerActive)
        {
            // тут хилим щит
            if (_HpCurretSphere <= _HpSphere)
            {
                _HpCurretSphere += 0.01f;
                HpLine.DrawCallRoadHp(_HpCurretSphere, _HpSphere);
            }
            if (!_timerActive && _HpCurretSphere >= _HpSphere / 1.6)
            {
                _timerActive = true;
                if (_enemyList.Count >= 1)
                    ActicateSlowM(true);
            }
        }
        else if (_timerActive && _enemyList.Count == 0) 
        {
            if (_HpCurretSphere <= _HpSphere)
            {
                _HpCurretSphere += 0.01f;
                HpLine.DrawCallRoadHp(_HpCurretSphere, _HpSphere);
            }
        }
    }


    /// <summary>
    /// убираем или ставим замедление на врагов
    /// </summary>
    /// <param name="activ">true = активно замедление</param>
    void ActicateSlowM(bool activ) 
    {
        foreach (EnemyObj temp in _enemyList) 
        {
            temp.SlowTimeEnable(activ);
        }
    }

    // добавляем или удаляем Enemy вошедших в Sphere
    void AddEnemy(EnemyObj enemy) 
    {
        if(!_enemyList.Contains(enemy))
            _enemyList.Add(enemy);
    }
    // TODO: обьекты сразу не удаляются (переписать контроль используя Vector3.SqrtDistance )
    public void RemoveEnemy(EnemyObj enemy) 
    {
        _enemyList.Remove(enemy);
    }



}
