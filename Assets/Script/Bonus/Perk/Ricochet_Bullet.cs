using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet_Bullet : MonoBehaviour, IPerk
{  
    public void InitPerk()
    {
        BulletSource.SetModifyBullet("Ricochet");
    }
    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
