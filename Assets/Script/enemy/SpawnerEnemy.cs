using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject Enemy;
    public int EnemyCount = 0;

    private void Start()
    {
        InvokeRepeating("ChoiseSite",0f,1f);
    }
                                 //x-x    z   -  z 
    float[,] SiteCoordinate = { {15f,16f,10f,-10.5f },  //вверх
                                {-15f,-16f,10f,-10.5f}, //вниз
                                {-15f,16f,10f,10.5f },     //Влево
                                {-15,16,-10f,-10.5f } };    //вправо

    int I_danger = 0;

    void ChoiseSite() 
    {
        if (EnemyCount <= 10)
        {
            EnemyCount++;
            int i = Random.Range(0, 4);
            Spawn(Random.Range(SiteCoordinate[i, 0], SiteCoordinate[i, 1]), Random.Range(SiteCoordinate[i, 2], SiteCoordinate[i, 3]));
        }
    }

    void Spawn(float x,float z) 
    {
        I_danger++;
        GameObject temp = Instantiate(Enemy,new Vector3(x,0,z),new Quaternion());
        EnemyAI tempAi = temp.GetComponent<EnemyAI>();
        tempAi.GoTo = gameObject.transform;
        if (I_danger >= 2 && Random.Range(0, 50) >= 25)
        {
            I_danger = 0;
            tempAi.MoveSpeed = 2.2f;
            tempAi.Dnager = true;
        }
        else 
        {
            tempAi.MoveSpeed = 1.5f;
            tempAi.Dnager = false;

        }
    }
}
