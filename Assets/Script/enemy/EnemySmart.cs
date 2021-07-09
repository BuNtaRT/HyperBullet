using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmart : EnemyAIBase
{

    protected override Color SetColor()
    {
        return new Color(1,0.69f,0,0.4f);
        
    }
    protected override float SetSpeed()
    {
        return 1;
    }

    protected override bool Dodge()
    {
        if (Random.Range(0, 101) >= 0)
        {
            Debug.Log("DODGE");

            InitDodge();
            return true;
        }
        else
        {
            return false;
        }
    }

    void InitDodge()
    {
        //float[] degree = new float[4] { -0.15f,0.25f, 0.15f, -0.25f };
        //float ran = degree[Random.Range(0, degree.Length)];
        Vector3 pos = gameObject.transform.position;
        //float radius = Vector3.Distance(gameObject.transform.position,GoTo.position)+0.1f;


        //float rand = Random.Range(0, 0.1f);


        //float xA = Vector3.Angle(pos.normalized.x);
        //float zA = Mathf.Cos(pos.normalized.z);
        //float xN = pos.normalized.x;
        //float zZ = pos.normalized.z;

        //Vector3 newPos = new Vector3(( x +rand) * radius, pos.y,(z + rand) * radius);
        //Vector3 newPos = new Vector3(Mathf.Sin(pos.normalized.x + ran) * radius, pos.y,Mathf.Cos(pos.normalized.z) * radius);


        //RePosition(newPos);
    }
}
