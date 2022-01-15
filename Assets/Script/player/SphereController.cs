using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    //отвечает за хп и макс хп сферы, отключает или включает slowMotion у врагов, а так же отрисовывает свое хп на внутриигровом интерфейсе Road

    float _hpSphere = 5;
    float _hpCurretSphere;
    bool  _timerActive = false;
    float _powerHealing = 0.01f;
    public SpriteRenderer  MaskRoad;
    [SerializeField] SpriteRenderer  SphereSp;
    [SerializeField] SpriteRenderer  LightColor;
    List<EnemyObj>         _enemyList = new List<EnemyObj>();   // те кто подошел достаточно близко


    public static SphereController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance obj over 1");

        GlobalEventsManager.OnEnemyKill.AddListener(RemoveEnemy);
    }

    private void Start()
    {
        _hpCurretSphere = _hpSphere;
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

            _hpCurretSphere -= 0.02f;

            HpLine.DrawCallRoadHp(_hpCurretSphere, _hpSphere);
            if (_hpCurretSphere <= 0)
            {
                _timerActive = false;
                ActicateSlowM(false);
            }
        }
        else if (!_timerActive)
        {
            // тут хилим щит
            if (_hpCurretSphere <= _hpSphere)
            {
                _hpCurretSphere += _powerHealing;
                HpLine.DrawCallRoadHp(_hpCurretSphere, _hpSphere);
            }
            if (!_timerActive && _hpCurretSphere >= _hpSphere / 1.6)
            {
                _timerActive = true;
                if (_enemyList.Count >= 1)
                    ActicateSlowM(true);
            }
        }
        else if (_timerActive && _enemyList.Count == 0) 
        {
            if (_hpCurretSphere <= _hpSphere)
            {
                _hpCurretSphere += _powerHealing;
                HpLine.DrawCallRoadHp(_hpCurretSphere, _hpSphere);
            }
        }
    }

    /// <summary>
    /// All hp sphere boost
    /// </summary>
    /// <param name="x"> -1:1 value percent</param>
    public void SetXFactor(float x) 
    {
        _hpSphere = _hpSphere + _hpSphere * x;
        Debug.Log(_hpSphere + " hp sphere" );
    }

    public Color SetColor(Color newColor) 
    {
        Color tempPast = SphereSp.color;
        SphereSp.color = newColor;
        LightColor.color = newColor;
        return tempPast;
    }

    /// <summary>
    /// Speed heal shealt
    /// </summary>
    /// <param name="x"> -1:1 percent boost</param>
    public void SetPowerHealing(float x) 
    {
        _powerHealing = _powerHealing + (_powerHealing * x);
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

    public void RemoveEnemy(Transform enemy) 
    {
        _enemyList.Remove(enemy.GetComponent<EnemyObj>());
    }
}
