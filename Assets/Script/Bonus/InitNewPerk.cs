using System;
using UnityEngine;

public class InitNewPerk : MonoBehaviour
{
    GameObject _objForPerk;
    SpawnPerks _spawnPerk;  // говорим что ему что бы этот перк больше не выпадал 
    private void Awake()
    {
        _spawnPerk = gameObject.GetComponent<SpawnPerks>();
    }
    public void InitPerk(PerkSO curretPerk)
    {
        _spawnPerk.PickItem(curretPerk.name);
        _objForPerk?.GetComponent<IPerk>().DestroyPerk();
        // создаем тип по имени из enum.perkName 
        //Type typeObj = Type.GetType(curretPerk.perkName.ToString());
        //_currPerk = (IPerk)Activator.CreateInstance(typeObj);
        //_objForPerk.AddComponent(_currPerk.GetType());

        _objForPerk = Instantiate(Resources.Load<GameObject>(ResourcePath.PERK_OBJ + curretPerk.perkName.ToString()));
        _objForPerk.GetComponent<IPerk>().InitPerk();

    }
}
