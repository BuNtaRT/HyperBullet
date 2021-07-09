using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    sbyte _damage = 2;
    byte hp = 1;
    Sequence _seqenceMove;
    float _speed = 1;
    public void Init(Vector3 end) 
    {
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
    void Die() 
    {
        _seqenceMove.Kill();
        gameObject.SetActive(false);
    }

}
