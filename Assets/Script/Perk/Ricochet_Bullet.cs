using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet_Bullet : MonoBehaviour, IPerk
{  
    public void InitPerk()
    {
        BulletBase.SetColor(new Color(0.1650943f, 1f, 0.3047963f).ConvertToGradient());
        BulletBase.SetModifyBullet("Ricochet");
    }

    public void DestroyPerk()
    {
        BulletBase.SetDefault();
        Destroy(gameObject);
    }
}
