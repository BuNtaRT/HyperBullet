using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{

    protected EnemyObj   _enemyObj          ;
    public    bool       InSphere = false   ;
    public    Transform  GoTo               ;
    private   Vector3    _enemySpawnPosition;
    private   sbyte      _hp   = 2          ;
    private   bool       _pause = false     ;

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

    public void Attack() 
    {
        _enemyObj.Attack();
    }

    public void SlowTimeEnable(bool enable) 
    {
        _enemyObj.SlowTimeEnable(enable);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ExplBullet"))
        {
            if (!Dodge())
            {
                MinusHp(2);
            }
        }
    }

    private void Die()
    {
        StopAllCoroutines();
        gameObject.GetComponent<Animator>().enabled = true;
        _enemyObj.Die();
    }

    private void OnTriggerEnter(Collider other) 
    {

        if (other.CompareTag("Bullet"))
        {
            // уклонение используется в EnemySmart
            if (!Dodge())
            {
                var tuple = other.GetComponent<Bullet>().CollisionEnemy(transform.position);
                sbyte damage = tuple.Item1;
                MinusHp(damage);
                if (tuple.Item2 != "" && _hp >= 0)
                {
                    Invoke(tuple.Item2, 0);
                }
            }
        }
        else if (other.CompareTag("Spell"))
        {
            MinusHp(64);
        }
    }

    private float _timeToPointPlayer;
    private IEnumerator Go()
    {
        float distance = Vector3.Distance(_enemySpawnPosition, GoTo.position);
        _timeToPointPlayer = distance / _enemyObj.MoveSpeed;

        float timeSteap = 0f;
        while (timeSteap < 1.0f)
        {
            timeSteap += Time.deltaTime / _timeToPointPlayer / _enemyObj.SpeedAnim;
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

/*------------------------Modification-------------------------------*/
    private float _timeEmi;
    private bool  _emiEnable = false;
    private void EmiBullet() 
    {
        if (!_emiEnable)
            StartCoroutine(EmiDebaff());
        else
            _timeEmi += 1;
    }

    private IEnumerator EmiDebaff() 
    {
        _timeEmi = 1;
        _emiEnable = true;
        float timetemp = _timeToPointPlayer;
        _timeToPointPlayer = 100;
        Animator enemyAnimator = gameObject.GetComponent<Animator>();
        enemyAnimator.enabled = false;
        yield return new WaitForSeconds(_timeEmi);
        enemyAnimator.enabled = true;
        _emiEnable = false;
        _timeToPointPlayer = timetemp;
    }

//-----------------------------------------------------------------------
    private void SlowSpeed() 
    {
        StartCoroutine(SlowONSec());
    }

    private IEnumerator SlowONSec() 
    {
        _timeToPointPlayer = _timeToPointPlayer * 4;
        yield return new WaitForSeconds(1f);
        _timeToPointPlayer = _timeToPointPlayer / 4;
    }
}
