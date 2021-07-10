using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{
    public Transform GoTo;

    Vector3 _enemySpawnPosition;

    protected EnemyObj _enemyObj;

    public bool InSphere = false;

    sbyte _hp = 2;

    void Start()
    {
        _enemyObj = gameObject.GetComponent<EnemyObj>();
        _enemyObj.ReInit(this);
        _enemyObj.SetColor(SetColor());
        _enemyObj.SetSpeed(SetSpeed());
        _hp = SetHp();

        LateInit();
        _enemySpawnPosition = gameObject.transform.position;
        LookAt();

        LateInit();
        StartCoroutine(Go());
    }

    // init inheritance 
    protected virtual void LateInit() {}

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
        _hp  = (sbyte)(_hp - inputDamage);
        if (_hp <=0)
        {
            Die();
        }
    }

    public void ShowCastScene(bool cast)
    {
        _enemyObj.ShowCastScene(cast);
    }
    public void Die()
    {
        StopAllCoroutines();
        _enemyObj.Die();
        //try
        //{
        //    DestroyImmediate(this);
        //    Debug.LogWarning("Destroy");
        //}
        //catch (System.Exception e)
        //{

        //    Debug.LogError("Not del  " + e);
        //}
    }
    public void Attack() 
    {
        _enemyObj.Attack();
    }

    public void SlowTimeEnable(bool enable) 
    {
        _enemyObj.SlowTimeEnable(enable);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Bullet"))
        {
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
        float time = distance / _enemyObj.MoveSpeed;

        float timeSteap = 0f;
        while (timeSteap < 1.0f)
        {
            timeSteap += Time.deltaTime / time / _enemyObj.SpeedAnim;
            gameObject.transform.position = Vector3.Lerp(_enemySpawnPosition, GoTo.position, timeSteap);
            yield return null;
        }
        yield return new WaitForSeconds(0);
    }

    protected void RePosition()
    {
        StopAllCoroutines();
        _enemySpawnPosition = transform.position;
        LookAt();
        StartCoroutine(Go());
    }
}
