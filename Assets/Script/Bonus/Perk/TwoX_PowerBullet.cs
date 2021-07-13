using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoX_PowerBullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletSource.SetColor(ColorToGradient.Convert(new Color(0, 0.6f, 1f)));
        BulletSource.SetHp(2);
    }

    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
