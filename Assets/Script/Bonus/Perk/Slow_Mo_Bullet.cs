using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Mo_Bullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletSource.SetDebaffEnemy("SlowSpeed");
    }
    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
