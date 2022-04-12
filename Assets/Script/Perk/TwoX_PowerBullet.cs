using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoX_PowerBullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletBase.SetColor(new Color(0, 0.6f, 1f).ConvertToGradient());
        BulletBase.SetHp(2);
    }

    public void DestroyPerk()
    {
        BulletBase.SetDefault();
        Destroy(gameObject);
    }
}
