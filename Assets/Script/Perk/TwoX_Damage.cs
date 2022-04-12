using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoX_Damage : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletBase.SetDamage(4);
        BulletBase.SetColor(new Color(1,0.043f,0).ConvertToGradient());
    }

    public void DestroyPerk()
    {
        BulletBase.SetDefault();
        Destroy(gameObject);
    }
}
