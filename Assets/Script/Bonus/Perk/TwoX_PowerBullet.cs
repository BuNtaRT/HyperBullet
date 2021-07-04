using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoX_PowerBullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        Debug.Log("StartTwoX_PowerBullet");
    }

    public void DisablePerk()
    {
        Debug.Log("Disable TwoX_PowerBullet");
    }

    public void DestroyPerk()
    {
        Debug.Log("Destroy TwoX_PowerBullet");
    }
}
