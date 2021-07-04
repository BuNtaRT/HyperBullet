using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class SpawnPerk : MonoBehaviour
{
    // спавнит перк из поверженного противника

    [SerializeField] private Dictionary<string, bool> perks;     // имя PerkSO файла / был ли он выран ранее
    public GameObject TemplatePerk;

    // переиспользуемый обьект для отображения перка на дороге
    GameObject perkObject;
    private int tempPerkForChoise;


    //загружаем доступные перки
    private void Awake()
    {
        perks = new Dictionary<string, bool>();
        Object[] tempPerk = Resources.LoadAll(ResourcePath.pathToPerkSO, typeof(PerkSO));
        foreach (PerkSO temp in tempPerk)
        {
            if (AccessibilityPerk(temp.name))
                perks.Add(temp.name,false);
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


    [SerializeField] PerksUI PerksUI;
    void CallPerkUI(PerkSO choisedPerk) 
    {
        PerksUI.PerkNow = choisedPerk;
    }

    public void spawnPerk(Vector3 TargetCoordinate)
    {
        // если наш обьект еще лежит на дороге то ничего не делам
        if (perkObject.activeSelf == true)
            return;

        var sortedPerk = (from st in perks
                         where st.Value == false
                         select st.Key).ToList();

        if (sortedPerk.Count < 3) 
        {
            // если доступно 2 перка то мы все равно выберим 1 но все остальные восстановим
            var tempMainDic = new Dictionary<string, bool>();
            Debug.LogWarning("--avalible perk count = " + sortedPerk.Count);
            foreach (var temp in perks) 
            {
                tempMainDic.Add(temp.Key,false);
            }
            perks = tempMainDic;
            Debug.LogWarning("--Fixed");
        }

        string randomPerkValue = sortedPerk[Random.Range(0,sortedPerk.Count)];
        PerkSO newPerk = Resources.Load<PerkSO>(ResourcePath.pathToPerkSO + randomPerkValue);

        Debug.Log("spawn perk");
        // спавним собственно
        perkObject.transform.position = TargetCoordinate;
        perkObject.transform.eulerAngles = new Vector3(-90, 0, Random.Range(-10, -140));
        perkObject.transform.Find("Ico").GetComponent<SpriteRenderer>().sprite = newPerk.Ico;
        perkObject.SetActive(true);
        

        CallPerkUI(newPerk);

    }
}
