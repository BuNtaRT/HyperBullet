using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PerkNI : MonoBehaviour
{
    [SerializeField] GameObject     _perkGameObj;
    [SerializeField] SpriteRenderer _perkIco;
    public static    PerkNI         Instance;
    IPerk _currPerk;


    private void Awake()
    {
        Instance = this;
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
        // создаем тип по имени из enum.perkName 
        Debug.Log(curretPerk.perkName.ToString());
        Type typeObj = Type.GetType(curretPerk.perkName.ToString());
        _currPerk = (IPerk)Activator.CreateInstance(typeObj);
        _currPerk.InitPerk();
    }

    public void ShowAndInitPerk(PerkSO curretPerk) 
    {
        Show(curretPerk);
        InitPerk(curretPerk);
    }


}
