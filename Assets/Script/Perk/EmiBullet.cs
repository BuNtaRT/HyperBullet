using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmiBullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletBase.SetColor(new Color(1, 0.96f, 0.71f).ConvertToGradient());
        BulletBase.SetDamage(1);
        BulletBase.SetSpeed(2);
        BulletBase.SetDebaffEnemy("EmiBullet");
    }

    public void DestroyPerk()
    {
        BulletBase.SetDefault();
        Destroy(gameObject);
    }
}
