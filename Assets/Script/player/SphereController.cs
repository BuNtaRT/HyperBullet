using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    //отвечает за хп и макс хп сферы, отключает или включает slowMotion у врагов, а так же отрисовывает свое хп на внутриигровом интерфейсе Road

    float HpSphere = 5;
    float HpCurretSphere;
    public SpriteRenderer maskRoad;
    bool timerActive = false;
    List<EnemyAI> enemyList = new List<EnemyAI>();

    public static SphereController Sphere { get; private set; }

    private void Awake()
    {
        Sphere = this;
    }

    private void Start()
    {
        HpCurretSphere = HpSphere;
        HpLine.init(maskRoad);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy") 
        {
            EnemyAI temp = other.GetComponent<EnemyAI>();
            if (!enemyList.Contains(temp))
            {
                AddEnemy(other.GetComponent<EnemyAI>());
                temp.Attack();                      // запускаем анимацию аттаки
                temp.SlowTimeEnable(timerActive);
                enemyList.Add(temp);                // добавляем что бы деактивировать замедление когда хп пропадет 
            }
        }
    }

    private void FixedUpdate()
    {
        if (timerActive && enemyList.Count >= 1)
        {
            // тут тратим пока врагов не останется или пока щит не кончится 
            if (enemyList.Count == 0)
                timerActive = false;

            HpCurretSphere -= 0.02f;

            HpLine.DrowCallRoadHp(HpCurretSphere, HpSphere);
            if (HpCurretSphere <= 0)
            {
                timerActive = false;
                Deactivate(true);
            }
        }
        else if(!timerActive)
        {
            // тут хилим щит
            if (HpCurretSphere <= HpSphere)
            {
                HpCurretSphere += 0.01f;
                HpLine.DrowCallRoadHp(HpCurretSphere,HpSphere);
            }
            if (!timerActive && HpCurretSphere >= HpSphere / 1.6)
            {
                timerActive = true;
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
        foreach (EnemyAI temp in enemyList) 
        {
            temp.SlowTimeEnable(!deactiv);
        }
    }

    // добавляем или удаляем Enemy вошедших в Sphere
    void AddEnemy(EnemyAI enemy) 
    {
        enemyList.Add(enemy);
    }
    public void RemoveEnemy(EnemyAI enemy) 
    {
        enemyList.Remove(enemy);
    }



}
