using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoX_PowerBullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        Gradient trailColor = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[1];
        colorKey[0].color   = new Color(0, 0.6f, 1f);
        colorKey[0].time    = 0f;
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha   = 1;
        alphaKey[0].time    = 0;
        trailColor.SetKeys(colorKey, alphaKey);

        BulletSource.SetColor(trailColor);
        BulletSource.SetHp(2);
    }

    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
