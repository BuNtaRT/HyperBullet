using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplBullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletSource.SetModifyBullet("Explosion");
        BulletSource.SetColor(ColorToGradient.Convert(new Color(0.95f,0,1)));
    }
    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
