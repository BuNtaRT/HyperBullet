using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoX_Damage : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletSource.SetDamage(4);
        BulletSource.SetColor(ColorToGradient.Convert(new Color(1,0.043f,0)));
    }
    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
