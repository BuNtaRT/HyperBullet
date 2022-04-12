using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplBullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletBase.SetModifyBullet("Explosion");
        BulletBase.SetColor(new Color(0.95f,0,1).ConvertToGradient());
    }
    public void DestroyPerk()
    {
        BulletBase.SetDefault();
        Destroy(gameObject);
    }
}
