using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmart : EnemyAIBase
{
    private float _angleSave;

    protected override Color SetColor()
    {
        return ColorEnemy.Yellow;
    }

    protected override float SetSpeed()
    {
        return 1.5f + PlayerPrefs.GetFloat(PlayerPKey.SPEED_UP);
    }

    protected override bool Dodge()
    {
        if (Random.Range(0, 101) >= 50 && !InSphere)
        {
            InitDodge();
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void LateInit()
    {
        _angleSave = Mathf.Atan2(transform.position.x,transform.position.z)* Mathf.Rad2Deg;
    }

    private void InitDodge()
    {
        if((Random.Range(0,51) >= 25 || _angleSave <=21) && _angleSave <= 339)
            SetRot(_angleSave + Random.Range(10, 20));
        else
            SetRot(_angleSave + Random.Range(-10,-20));
        RePosition();
    }

    private void SetRot(float angle) 
    {
        _angleSave = angle;
        float radius = Vector3.Distance(GoTo.position, transform.position);
        Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * (angle)) * radius, 0, Mathf.Cos(Mathf.Deg2Rad * (angle)) * radius);
        transform.position = direction;
    }
}
