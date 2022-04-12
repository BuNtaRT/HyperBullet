using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public  bool          IsSplinet;      // если осколок то не применяются модификации
    private sbyte         _hp     = 1;
    private sbyte         _damage = 2;
    private float         _speed  = 1;
    private string        _methodDebaffEnemy  = "";
    private string        _methodModifyBullet = "";
    private Sequence      _seqenceMove;
    private TrailRenderer _trail;
    private Vector3       _enemyPosition;

    private void Awake()
    {
        _trail = gameObject.GetComponent<TrailRenderer>();
        GlobalEventsManager.OnPause.AddListener(PauseSub);
    }

    private void PauseSub(PauseStatus status, bool enable)
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

    private void SetColorTrail(Gradient gradient) 
    {
        _trail.colorGradient = gradient;
    }

    public Tuple<sbyte,string> CollisionEnemy(Vector3 enemyPosition) 
    {
        _enemyPosition = enemyPosition; 
        TakeDamage();
        return Tuple.Create(_damage, _methodDebaffEnemy);
    }

    private void TakeDamage() 
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

    private bool die = false;
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
    private void BurstingSimple() 
    {
        int rand = Random.Range(1, 3);
        for (int i = 0; i <= rand; i++)
        {
            SpawnNewBull(5,true);
        }
    }
    private void Ricochet() 
    {
        SpawnNewBull(20,false);
    }

    private void BurstingSuper() 
    {
        int rand = Random.Range(2, 4);
        for (int i = 0; i <= rand; i++)
        {
            SpawnNewBull(15, false);
        }
    }

    private void Explosion() 
    {
        _enemyPosition = new Vector3(_enemyPosition.x, 0.4f, _enemyPosition.z);
        ObjPool.Instance.SpawnObj(TypeObj.ExplosionFromBullet, _enemyPosition);
    }

    private void SpawnNewBull(float radius, bool isSplint) 
    {
        Vector3 posNewSplinter = new Vector3(Random.Range(-1f, 1f) * radius, 1, Random.Range(-1f, 1f) * radius);
        posNewSplinter += transform.position;
        Transform temp = ObjPool.Instance.SpawnObj(TypeObj.Bullet, _enemyPosition);
        temp.GetComponent<Bullet>().Init(posNewSplinter);
        temp.GetComponent<Bullet>().IsSplinet = isSplint;
    }
}
