using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{
    public Transform GoTo;

    Vector3 _enemySpawnPosition;

    EnemyObj enemyObj;

    sbyte _hp = 2;

    void Start()
    {
        enemyObj = gameObject.GetComponent<EnemyObj>();
        enemyObj.ReInit(this);
        enemyObj.SetColor(SetColor());
        enemyObj.SetSpeed(SetSpeed());
        _hp = SetHp();

        _enemySpawnPosition = gameObject.transform.position;
        LookAt();

        StartCoroutine(Go());
    }

    protected virtual Color SetColor() { return new Color(0, 0, 0, 1); }

    protected virtual float SetSpeed() { return 0; }

    protected virtual sbyte SetHp() { return 2; }

    protected virtual void LookAt() 
    {
        transform.LookAt(GoTo);
    }

    protected virtual bool Dodge() 
    {
        return false;
    }

    protected virtual void MinusHp(sbyte inputDamage) 
    {
        if (inputDamage >= _hp)
            Die();
        else
            _hp = (sbyte)(_hp - inputDamage);
    }

    public void ShowCastScene(bool cast)
    {
        enemyObj.ShowCastScene(cast);
    }
    public void Die()
    {
        StopAllCoroutines();
        enemyObj.Die();
    }
    public void Attack() 
    {
        enemyObj.Attack();
    }

    public void SlowTimeEnable(bool enable) 
    {
        enemyObj.SlowTimeEnable(enable);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Bullet")) 
        {
            Debug.Log("bullet!!");
            // уклонение используется в EnemySmart
            if (!Dodge())
            {
                sbyte damage = other.GetComponent<Bullet>().CollisionEnemy();
                MinusHp(damage);
            }
        }
    }

    IEnumerator Go()
    {
        float distance = Vector3.Distance(_enemySpawnPosition, GoTo.position);
        float time = distance / enemyObj.MoveSpeed;

        while (_ienumProgress < 1.0f)
        {
            _ienumProgress += Time.deltaTime / time / enemyObj.SpeedAnim;
            gameObject.transform.position = Vector3.Lerp(_enemySpawnPosition, GoTo.position, _ienumProgress);
            yield return null;
        }
        yield return new WaitForSeconds(0);
    }

    float _ienumProgress = 0;
    protected void RePosition(Vector3 newPosition)
    {
        StopAllCoroutines();
        transform.position = newPosition;
        _ienumProgress =0;
        _enemySpawnPosition = newPosition;
        StartCoroutine(Go());
        LookAt();
    }
}
