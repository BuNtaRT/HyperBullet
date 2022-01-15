using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Playables;

public class LoadLvl : MonoBehaviour
{
    [SerializeField]
    PlayableAsset _introScene;
    [SerializeField]
    GameObject _mapContainet;
    [SerializeField]
    GameObject _playerObj;
    public GameObject   SpawnerEnemy ;
    public SpawnSpell   SpawnerSpells;
           bool         _coldStart   ;


    private void Awake()
    {
        LoadEnemyFromConfig();
        LoadCurretSpell();

        BulletBase.SetDefault();

        SetMap();
    }

    void Start()
    {
        StartIntro();
    }

    #region Load Curret Spell
    void LoadCurretSpell() 
    {
        List<SpellSO> spells = new List<SpellSO>();

        if (File.Exists(ApplicationPaths.Spells))
        {
            using (StreamReader sr = new StreamReader(ApplicationPaths.Spells))
            {
                while (sr.Peek() >=0)
                {
                    spells.Add(Resources.Load<SpellSO>(ResourcePath.SPELL_SO + sr.ReadLine()));
                }
            }
        }
        foreach (var item in spells)
        {
            Debug.Log("Peek SO: " + item.NameSc);
        }
        SpawnerSpells.SetSpellList(spells);
    }
    #endregion

    #region Load Enemy config
    struct EnemyLoad
    {
        public string Type;
        public int    Count;
    }
    void LoadEnemyFromConfig() 
    {
        List<EnemyLoad> enemyList = new List<EnemyLoad>();
        XmlDocument loadXml = new XmlDocument();
        TextAsset xmlFile   = Resources.Load<TextAsset>(ResourcePath.LVL_CONFIG + PlayerPrefs.GetInt(PlayerPKey.LVL));

        loadXml.LoadXml(xmlFile.text);
        XmlElement rootXml = loadXml.DocumentElement;

        foreach (XmlNode temp in rootXml)
        {
            EnemyLoad tempEnemy = new EnemyLoad();
            if (temp.Attributes.Count > 0)
            {
                tempEnemy.Type = temp.Attributes.GetNamedItem("type").Value;
            }

            tempEnemy.Count = XmlConvert.ToInt32(temp.SelectSingleNode("count").InnerText);

            if (tempEnemy.Count != 0)
                enemyList.Add(tempEnemy);
        }
        SendToSpawner(enemyList);
    }
    void SendToSpawner(List<EnemyLoad> enemyList) 
    {
        SpawnerEnemy spawner = SpawnerEnemy.GetComponent<SpawnerEnemy>();
        foreach (EnemyLoad tempLoad in enemyList) 
        {
            GameObject tempObjBeh = Instantiate(Resources.Load<GameObject>(ResourcePath.ENEMY_BEHAIVOR + tempLoad.Type));
            EnemyAIBase tempeEnemyAI = tempObjBeh.GetComponent<EnemyAIBase>();
            spawner.AddNewBehaivor(tempeEnemyAI,tempLoad.Count);
            //Destroy(tempObjBeh);
        }
    }
    #endregion

    #region Load Visual
    public void ColdStart(bool val) => _coldStart = val;
    void SetMap()
    {
        Debug.Log(ResourcePath.LVLS + "Map1");
        GameObject map = Instantiate(Resources.Load<GameObject>(ResourcePath.LVLS + "Map1"), _mapContainet.transform);
    }

    void StartIntro()
    {
        if (_coldStart)
            EndIntro();
        else
            CutScene.Instance.Show(Resources.Load<PlayableAsset>(ResourcePath.CUT_SCENE + "IntroLvl1"), EndIntro);
    }
    void EndIntro()
    {
        _playerObj.SetActive(true);
        SpawnerEnemy.SetActive(true);
        RuntimeUI.Instance.ShowStartLogo();
    }
    
    #endregion


}
