using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    //отвечает за хп и макс хп сферы, отключает или включает slowMotion у врагов, а так же отрисовывает свое хп на внутриигровом интерфейсе Road

    float _HpSphere = 5;
    float _HpCurretSphere;
    bool  _timerActive = false;
    public SpriteRenderer MaskRoad;
    List<EnemyObj>         enemyList = new List<EnemyObj>();

    public static SphereController Sphere { get; private set; }

    private void Awake()
    {
        Sphere = this;
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
            if (!enemyList.Contains(temp) && temp!=null)
            {
                AddEnemy(other.GetComponent<EnemyObj>());
                temp.Attack();                      // запускаем анимацию аттаки
                temp.SlowTimeEnable(_timerActive);
                enemyList.Add(temp);                // добавляем что бы деактивировать замедление когда хп пропадет 
            }
        }
    }

    private void FixedUpdate()
    {
        if (_timerActive && enemyList.Count >= 1)
        {
            // тут тратим пока врагов не останется или пока щит не кончится 
            if (enemyList.Count == 0)
                _timerActive = false;

            _HpCurretSphere -= 0.02f;

            HpLine.DrawCallRoadHp(_HpCurretSphere, _HpSphere);
            if (_HpCurretSphere <= 0)
            {
                _timerActive = false;
                Deactivate(true);
            }
        }
        else if(!_timerActive)
        {
            // тут хилим щит
            if (_HpCurretSphere <= _HpSphere)
            {
                _HpCurretSphere += 0.01f;
                HpLine.DrawCallRoadHp(_HpCurretSphere,_HpSphere);
            }
            if (!_timerActive && _HpCurretSphere >= _HpSphere / 1.6)
            {
                _timerActive = true;
                if (enemyList.Count >= 1)
                    Deactivate(false);
            }
        }
    }



    /// <summary>
    /// убираем или ставим замедление на врагов
    /// </summary>
    /// <param name="deactiv">false = активно замедление</param>
    void Deactivate(bool deactiv) 
    {
        foreach (EnemyObj temp in enemyList) 
        {
            temp.SlowTimeEnable(!deactiv);
        }
    }

    // добавляем или удаляем Enemy вошедших в Sphere
    void AddEnemy(EnemyObj enemy) 
    {
        if(!enemyList.Contains(enemy))
            enemyList.Add(enemy);
    }
    // TODO: обьекты сразу не удаляются (переписать контроль используя Vector3.SqrtDistance )
    public void RemoveEnemy(EnemyObj enemy) 
    {
            enemyList.Remove(enemy);
    }



}
