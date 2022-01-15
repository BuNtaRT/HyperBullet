using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperDiscontr : MonoBehaviour,IPerk
{ 
    public void InitPerk()
    {
        Gradient trailColor = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[1];
        colorKey[0].color = new Color(0.6454144f, 0f, 1f);
        colorKey[0].time = 0f;
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1;
        alphaKey[0].time = 0;
        trailColor.SetKeys(colorKey, alphaKey);

        BulletBase.SetColor(trailColor);
        BulletBase.SetModifyBullet("BurstingSuper");
    }
    public void DestroyPerk()
    {
        BulletBase.SetDefault();
        Destroy(gameObject);
    }


}
