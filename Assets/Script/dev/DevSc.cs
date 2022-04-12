using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevSc : MonoBehaviour
{
    [SerializeField]
    private bool               _introColdStart;
    [SerializeField]
    private PlayerPKey.langKey _language;
    [SerializeField]
    private Transform          _fixTransform;
    [SerializeField]
    private int                _curretLvl;
    public Dropdown            SpellDropDown;
    [SerializeField]
    private float              _bulletOnSession;

    [SerializeField]
    private LoadLvl            _loaderLvl;

    private void Awake()
    {
        PlayerPrefs.SetInt(PlayerPKey.LVL, _curretLvl);
        PlayerPrefs.SetInt(PlayerPKey.LANGUAGE, (int)_language);
        PlayerPrefs.SetFloat(PlayerPKey.SPEED_UP, 0);
        PlayerPrefs.Save();
        _loaderLvl.ColdStart(_introColdStart);
    }

    private void Start()
    {
        SessionBullet.Instance.Add(_bulletOnSession);
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
        //GlobalEventsManager.SendSpell(temp);
    }

    private bool _pause = false;
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
