using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public bool   IsSplinet;      // если осколок то не применяются модификации
    sbyte         _hp     = 1;
    sbyte         _damage = 2;
    float         _speed  = 1;
    string        _methodDebaffEnemy  = "";
    string        _methodModifyBullet = "";
    Sequence      _seqenceMove;
    TrailRenderer _trail;
    Vector3      _enemyPosition;
    private void Awake()
    {
        _trail = gameObject.GetComponent<TrailRenderer>();
        GlobalEventsManager.OnPause.AddListener(PauseSub);
    }
    void PauseSub(PauseStatus status, bool enable)
    {
        if (status == PauseStatus.cutScene)
        {
            _trail.enabled = !enable;
        }
    }
    public void Init(Vector3 end) 
    {
        die       = false;
        IsSplinet = false;
        var conf  = BulletBase.GetConf();
        _hp       = conf.Hp;
        _damage   = conf.Damage;
        _speed    = conf.Speed;
        _methodDebaffEnemy  = conf.NameDebaffEnemy;
        _methodModifyBullet = conf.NameModifyBullet;
        SetColorTrail(conf.TrailColor);
        _seqenceMove.Append(transform.DOMove(end, _speed).OnComplete(() => { Die(); }));
    }

    void SetColorTrail(Gradient gradient) 
    {
        _trail.colorGradient = gradient;
    }

    public Tuple<sbyte,string> CollisionEnemy(Vector3 enemyPosition) 
    {
        _enemyPosition = enemyPosition; 
        TakeDamage();
        return Tuple.Create(_damage, _methodDebaffEnemy);
    }

    void TakeDamage() 
    {
        if (_methodModifyBullet != "" && !IsSplinet)
        {
            Invoke(_methodModifyBullet,0);
        }
        MinusHp();
    }

    void MinusHp() 
    {
        _hp--;
        if (_hp == 0) 
        {
            Die();
        }
    }

    bool die = false;
    void Die() 
    {
        if (!die)
        {
            die = true;
            _seqenceMove.Kill();
            ObjPool.Instance.Destroy(TypeObj.Bullet, gameObject);
        }
    }

    /*-----------------MODIFICATION-----------------*/
    //Create 2-3 bullet on one Arncle
    void BurstingSimple() 
    {
        int rand = Random.Range(1, 3);
        for (int i = 0; i <= rand; i++)
        {
            SpawnNewBull(5,true);
        }
    }
    void Ricochet() 
    {
        SpawnNewBull(20,false);
    }

    void BurstingSuper() 
    {
        int rand = Random.Range(2, 4);
        for (int i = 0; i <= rand; i++)
        {
            SpawnNewBull(15, false);
        }
    }

    void Explosion() 
    {
        _enemyPosition = new Vector3(_enemyPosition.x, 0.4f, _enemyPosition.z);
        ObjPool.Instance.SpawnObj(TypeObj.ExplosionFromBullet, _enemyPosition);
    }

    void SpawnNewBull(float radius, bool isSplint) 
    {
        Vector3 posNewSplinter = new Vector3(Random.Range(-1f, 1f) * radius, 1, Random.Range(-1f, 1f) * radius);
        posNewSplinter += transform.position;
        Transform temp = ObjPool.Instance.SpawnObj(TypeObj.Bullet, _enemyPosition);
        temp.GetComponent<Bullet>().Init(posNewSplinter);
        temp.GetComponent<Bullet>().IsSplinet = isSplint;
    }
}
