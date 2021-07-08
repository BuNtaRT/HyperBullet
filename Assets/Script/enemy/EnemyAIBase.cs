using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{
    public Transform GoTo;

    Vector3 _enemySpawnPosition;

    EnemyObj enemyObj;

    void Start()
    {
        enemyObj = gameObject.GetComponent<EnemyObj>();
        enemyObj.ReInit(this);
        enemyObj.SetColor(SetColor());
        enemyObj.SetSpeed(SetSpeed());

        _enemySpawnPosition = gameObject.transform.position;
        LookAt();

        StartCoroutine(Go());
    }

    protected virtual Color SetColor() { return new Color(0, 0, 0, 1); }

    protected virtual float SetSpeed() { return 0; }

    protected virtual void LookAt() 
    {
        transform.LookAt(GoTo);
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

    IEnumerator Go()
    {
        float distance = Vector3.Distance(_enemySpawnPosition, GoTo.position);
        float time = distance / enemyObj.MoveSpeed;

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / time / enemyObj.SpeedAnim;
            gameObject.transform.position = Vector3.Lerp(_enemySpawnPosition, GoTo.position, timeStep);
            yield return null;
        }
        yield return new WaitForSeconds(0);
    }

    //IEnumerator DieAnim() 
    //{


    //    float time = 1f;
    //    float timeStep = 0f;
    //    Vector3 roboAfterPos = gameObject.transform.position;



    //    while (timeStep < 1.0f)
    //    {
    //        timeStep += Time.deltaTime / time;
    //        gameObject.transform.position = Vector3.Lerp(roboAfterPos, roboBeforePos, timeStep);
    //        yield return null;
    //    }
    //}




}
