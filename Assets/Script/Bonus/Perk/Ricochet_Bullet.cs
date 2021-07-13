﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet_Bullet : MonoBehaviour, IPerk
{  
    public void InitPerk()
    {
        BulletSource.SetColor(ColorToGradient.Convert(new Color(0.1650943f, 1f, 0.3047963f)));
        BulletSource.SetModifyBullet("Ricochet");
    }
    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
