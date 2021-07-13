using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Mo_shoot : MonoBehaviour, IPerk
{   
    public void InitPerk()
    {
        Shoot.Instance.SetModified("SlowMo");
    }
    public void DestroyPerk()
    {
        Shoot.Instance.ClearModified();
        Destroy(gameObject);
    }


}
