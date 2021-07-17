using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmiBullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletSource.SetColor(ColorToGradient.Convert(new Color(1, 0.96f, 0.71f)));
        BulletSource.SetDamage(1);
        BulletSource.SetSpeed(2);
        BulletSource.SetDebaffEnemy("EmiBullet");
    }

    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
