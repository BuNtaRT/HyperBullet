using System.Collections.Generic;
using UnityEngine;

public class DevSc : MonoBehaviour
{
    [SerializeField]
    PlayerPKey.langKey language;


    void Awake()
    {
        PlayerPrefs.SetInt(PlayerPKey.LANGUAGE, (int)language);
        PlayerPrefs.SetFloat(PlayerPKey.SPEED_UP, 0);
        PlayerPrefs.Save();
    }


    public PerksUI PerkUI;
    public List<PerkSO> Perks = new List<PerkSO>(); 
    public void OnDropDownPerkSet(int index) 
    {
        PerkUI.PerkNow = Perks[index];
        PerkUI.PerkShow();
    }
}
