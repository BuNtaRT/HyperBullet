using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bursting_Bullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletBase.SetColor(new Color(1, 0f, 0.8318019f).ConvertToGradient());
        BulletBase.SetModifyBullet("BurstingSimple");
    }

    public void DestroyPerk()
    {
        BulletBase.SetDefault();
        Destroy(gameObject);
    }
}
