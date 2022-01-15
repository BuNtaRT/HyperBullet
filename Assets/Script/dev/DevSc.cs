using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevSc : MonoBehaviour
{
    [SerializeField]
    bool IntroColdStart;
    [SerializeField]
    PlayerPKey.langKey language;
    [SerializeField]
    Transform fixTransform;
    [SerializeField]
    int curretLvl;
    public Dropdown SpellDropDown;
    [SerializeField]
    float BulletOnSession;

    [SerializeField]
    LoadLvl loaderLvl;

    void Awake()
    {
        PlayerPrefs.SetInt(PlayerPKey.LVL, curretLvl);
        PlayerPrefs.SetInt(PlayerPKey.LANGUAGE, (int)language);
        PlayerPrefs.SetFloat(PlayerPKey.SPEED_UP, 0);
        PlayerPrefs.Save();
        loaderLvl.ColdStart(IntroColdStart);

    }
    private void Start()
    {
        SessionBullet.Instance.Add(BulletOnSession);
    }

    public PerksUI PerkUI;
    public List<PerkSO> Perks = new List<PerkSO>(); 
    public void OnDropDownPerkSet(int index) 
    {
        PerkUI.PerkNow = Perks[index];
        GlobalEventsManager.CallPerk();
    }

    public void OnDropDownSpellSelectt(int index)
    {
        Debug.Log("get data = "  + SpellDropDown.options[index].text);
        SpellSO temp = Resources.Load<SpellSO>(ResourcePath.SPELL_SO + SpellDropDown.options[index].text);
        GlobalEventsManager.SendSpell(temp);
    }

    bool _pause=false;
    public void CutPause() 
    {
        _pause = !_pause;
        GlobalEventsManager.InvokPause(PauseStatus.cutScene,_pause);
    }
    public void PausePause() 
    {
        _pause = !_pause;
        GlobalEventsManager.InvokPause(PauseStatus.pause, _pause);

    }
    public void PickPause() 
    {
        _pause = !_pause;
        GlobalEventsManager.InvokPause(PauseStatus.pickSpellDir, _pause);
    }
}
