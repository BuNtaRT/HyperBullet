using UnityEngine;

public class InitNewPerk : MonoBehaviour
{
    GameObject _objForPerk;
    public void InitPerk(PerkSO curretPerk)
    {
        _objForPerk?.GetComponent<IPerk>().DestroyPerk();
        // создаем тип по имени из enum.perkName 
        Debug.Log(curretPerk.perkName.ToString());
        //Type typeObj = Type.GetType(curretPerk.perkName.ToString());
        //_currPerk = (IPerk)Activator.CreateInstance(typeObj);
        //_objForPerk.AddComponent(_currPerk.GetType());

        _objForPerk = Instantiate(Resources.Load<GameObject>(ResourcePath.PERK_OBJ + curretPerk.perkName.ToString()));
        _objForPerk.GetComponent<IPerk>().InitPerk();
    }
}
