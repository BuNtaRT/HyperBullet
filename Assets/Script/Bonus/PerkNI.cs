using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkNI : MonoBehaviour
{
    [SerializeField] GameObject PerkGameObj;
    [SerializeField] SpriteRenderer PerkIco;
    GameObject perkObj;
    public static PerkNI Instance;


    private void Awake()
    {
        Instance = this;
    }
    //управление отображения перка на дороге а так же его активация и деактивация
    void Show(PerkSO curretPerk) 
    {
        if (curretPerk == null)
            PerkGameObj.SetActive(true);
        PerkGameObj.GetComponent<Animation>().Play("ShowPerkOnRoad");
        PerkIco.sprite = curretPerk.Ico;
        PerkGameObj.SetActive(true);
        //CurretScript = Instantiate(Resources.Load<GameObject>("Bonus/Perks/" + temp.NameScript));
    }
    void InitPerk(PerkSO curretPerk) 
    {
        if (perkObj != null) 
        {
            perkObj.GetComponent<IPerk>().DisablePerk();
            perkObj.GetComponent<IPerk>().DestroyPerk();
        }
        perkObj = Instantiate(Resources.Load<GameObject>(ResourcePath.pathToPerkObject + curretPerk.perkName.ToString()));

        perkObj.GetComponent<IPerk>().InitPerk();
    }

    public void ShowAndInitPerk(PerkSO curretPerk) 
    {
        Show(curretPerk);
        InitPerk(curretPerk);
    }


}
