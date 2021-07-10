using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    sbyte _damage = 2;
    sbyte hp = 1;
    Sequence _seqenceMove;
    float _speed = 1;
    public void Init(Vector3 end) 
    {
        die = false;
        _seqenceMove.Append(transform.DOMove(end, _speed).OnComplete(() => { Die(); }));
    }

    public sbyte CollisionEnemy() 
    {
        TakeDamage();
        return _damage;
    }

    void TakeDamage() 
    {
        MinusHp();
    }

    void MinusHp() 
    {
        hp--;
        if (hp == 0) 
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
