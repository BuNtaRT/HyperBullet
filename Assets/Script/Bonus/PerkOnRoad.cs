using System.Collections;
using UnityEngine;

public class PerkOnRoad : ItemOnRoadBase, IItemOnRoad
{
    private void Awake()
    {
        _objectT = TypeObj.None;
        InitAwake();
    }
    private void OnEnable()
    {
        InitAnim();
    }
    public void Pick()
    {
        GlobalEventsManager.CallPerk();
        End();
        gameObject.SetActive(false);
    }
}
