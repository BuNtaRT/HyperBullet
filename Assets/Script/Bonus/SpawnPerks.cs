using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class SpawnPerks : MonoBehaviour
{
    // спавнит перк из поверженного противника

    [SerializeField] private Dictionary<string, bool> _perks;        // имя PerkSO файла / был ли он выран ранее
    public                   GameObject               TemplatePerk;

    // переиспользуемый обьект для отображения перка на дороге
    GameObject perkObject;

    //загружаем доступные перки
    private void Awake()
    {
                 _perks   = new Dictionary<string, bool>();
        Object[] tempPerk = Resources.LoadAll(ResourcePath.PERK_SO, typeof(PerkSO));
        foreach (PerkSO temp in tempPerk)
        {
            if (AccessibilityPerk(temp.name))
                _perks.Add(temp.name,false);
        }
    }

    // сравнивает имя со списком доступных перков если есть отвечает true 
    bool AccessibilityPerk(string nameObj)
    {
        return true;
    }

    private void Start()
    {
        perkObject = Instantiate(TemplatePerk);
        perkObject.SetActive(false);
    }

    [SerializeField] PerksUI _perksUI;
    void CallPerkUI(PerkSO choisedPerk) 
    {
        _perksUI.PerkNow = choisedPerk;
    }

    public void Spawn(Vector3 TargetCoordinate)
    {
        // если наш обьект еще лежит на дороге то ничего не делам
        if (perkObject.activeSelf == true)
            return;

        var sortedPerk = (from st in _perks
                         where st.Value == false
                         select st.Key).ToList();
        Debug.Log("valible perk  = " + sortedPerk.Count);
        if (sortedPerk.Count < 3) 
        {
            // если доступно 2 перка то мы все равно выберим 1 но все остальные восстановим
            var tempMainDic = new Dictionary<string, bool>();
            Debug.LogWarning("--avalible perk count = " + sortedPerk.Count);
            foreach (var temp in _perks) 
            {
                tempMainDic.Add(temp.Key,false);
            }
            _perks = tempMainDic;
        }

        string randomPerkName  = sortedPerk[Random.Range(0, sortedPerk.Count)];
        PerkSO newPerk = Resources.Load<PerkSO>(ResourcePath.PERK_SO + randomPerkName);

        // спавним собственно
        perkObject.transform.position    = TargetCoordinate + Vector3.up;
        perkObject.transform.eulerAngles = new Vector3(-90, 0, Random.Range(-10, -140));
        perkObject.transform.Find("Ico").GetComponent<SpriteRenderer>().sprite = newPerk.Ico;
        perkObject.SetActive(true);

        CallPerkUI(newPerk);
    }


    public void PickItem(string NamePerk) 
    {
        _perks[NamePerk] = true;
    }
}
