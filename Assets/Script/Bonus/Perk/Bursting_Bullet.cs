using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bursting_Bullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletSource.SetModifyBullet("BurstingSimple");
    }

    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
