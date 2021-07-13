using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    sbyte _hp     = 1;
    sbyte _damage = 2;
    float _speed  = 1;
    string _methodModifyEnemy = "";
    Sequence _seqenceMove;
    TrailRenderer _Trail;
    private void Awake()
    {
        _Trail = gameObject.GetComponent<TrailRenderer>();
    }
    public void Init(Vector3 end) 
    {
        var conf = BulletSource.GetConf();
        _hp     = conf.Hp;
        _damage = conf.Damage;
        _speed  = conf.Speed;
        SetColorTrail(conf.TrailColor);
        die = false;
        _seqenceMove.Append(transform.DOMove(end, _speed).OnComplete(() => { Die(); }));
        _methodModifyEnemy = conf.NameModify;
    }

    void SetColorTrail(Gradient gradient) 
    {
        _Trail.colorGradient = gradient;
    }

    public Tuple<sbyte,string> CollisionEnemy() 
    {
        TakeDamage();
        return Tuple.Create(_damage, _methodModifyEnemy);
    }

    void TakeDamage() 
    {
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
}
