using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        ClonePlayer.Instance.ClonePerkEnable();
        Shoot.Instance.SetModified("PlayerClone1");
    }
    public void DestroyPerk()
    {
        ClonePlayer.Instance.ClonePerkDisable();
        Shoot.Instance.SetModified("PlayerCloneDisable");
        Destroy(gameObject);
    }
}
