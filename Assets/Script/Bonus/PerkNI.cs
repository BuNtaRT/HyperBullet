using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PerkNI : MonoBehaviour
{
    [SerializeField] GameObject     _perkGameObj;
    [SerializeField] SpriteRenderer _perkIco;
                     InitNewPerk    _initNewPerk;
    public static PerkNI Instance;

    private void Awake()
    {
        Instance = this;
        _initNewPerk = gameObject.GetComponent<InitNewPerk>();
    }


    //управление отображения перка на дороге а так же его активация и деактивация
    void Show(PerkSO curretPerk) 
    {
        if (curretPerk == null)
            _perkGameObj.SetActive(true);

        _perkGameObj.GetComponent<Animation>().Play("ShowPerkOnRoad");
        _perkIco.sprite = curretPerk.Ico;
        _perkGameObj.SetActive(true);
    }
    void InitPerk(PerkSO curretPerk) 
    {
        _initNewPerk.InitPerk(curretPerk);
    }

    public void ShowAndInitPerk(PerkSO curretPerk) 
    {
        Show(curretPerk);
        InitPerk(curretPerk);
    }


}
