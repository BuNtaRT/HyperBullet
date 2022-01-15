using System;
using UnityEngine;

public class InitNewPerk : MonoBehaviour
{
    GameObject _objForPerk;
    SpawnPerks _spawnPerk;
    private void Awake()
    {
        _spawnPerk = gameObject.GetComponent<SpawnPerks>();
    }
    public void InitPerk(PerkSO curretPerk)
    {
        _spawnPerk.PickItem(curretPerk.name);
        _objForPerk?.GetComponent<IPerk>().DestroyPerk();

        _objForPerk = Instantiate(Resources.Load<GameObject>(ResourcePath.PERK_OBJ + curretPerk.perkName.ToString()));
        _objForPerk.GetComponent<IPerk>().InitPerk();

    }
}
