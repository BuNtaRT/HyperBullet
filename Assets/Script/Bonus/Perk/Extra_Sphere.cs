using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra_Sphere : MonoBehaviour,IPerk
{
    public void InitPerk()
    {
        Debug.Log("StartExtra");
    }

    public void DisablePerk()
    {
        Debug.Log("Disable StartExtra");
    }

    public void DestroyPerk()
    {
        Debug.Log("Destroy StartExtra");
    }
}
