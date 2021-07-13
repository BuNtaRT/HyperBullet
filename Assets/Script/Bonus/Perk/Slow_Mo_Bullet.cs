using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Mo_Bullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletSource.SetColor(ColorToGradient.Convert(new Color(0, 0.4590983f, 1f)));
        BulletSource.SetDebaffEnemy("SlowSpeed");
    }
    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
