using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Mo_Bullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletSource.SetModify("SlowSpeed");
    }
    public void DestroyPerk()
    {
        throw new System.NotImplementedException();
    }
}
