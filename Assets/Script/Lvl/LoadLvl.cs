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
    GameObject    _mapContainet;
    [SerializeField]
    GameObject    _playerObj;

    public GameObject   SpawnerEnemy ;
    public SpawnSpell   SpawnerSpells;
           bool         _coldStart   ;
    ConfigLevel         _jsonConfig = null;

    [Serializable]
    class ConfigLevel
    {
        public Enemy[] RoboEnemy;
        public Enemy[] LazerEnemy;
        public string  Map;
        public bool    ColdStart;
        public string  CutScene;
    }
    [Serializable]
    class Enemy 
    {
        public string Name;
        public int Count;
    }
    private void Awake()
    {
        _jsonConfig = JsonUtility.FromJson<ConfigLevel>(Resources.Load<TextAsset>(ResourcePath.LVL_CONFIG + PlayerPrefs.GetInt(PlayerPKey.LVL)).text);

        LoadEnemy();
        LoadCurretSpell();

        BulletBase.SetDefault();

        LoadMap();
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

    void LoadEnemy() 
    {
        LoadRoboEnemy();
    }
    void LoadRoboEnemy() 
    {
        SpawnerEnemy spawner = SpawnerEnemy.GetComponent<SpawnerEnemy>();
        foreach (Enemy temp in _jsonConfig.RoboEnemy)
        {
                GameObject tempObjBeh = Instantiate(Resources.Load<GameObject>(ResourcePath.ENEMY_BEHAIVOR + temp.Name));
                EnemyAIBase tempeEnemyAI = tempObjBeh.GetComponent<EnemyAIBase>();
                spawner.AddNewBehaivor(tempeEnemyAI, temp.Count);
        }
    }

    #endregion

    #region Load Visual
    public void ColdStart(bool val) => _coldStart = val;
    void LoadMap()
    {
        Debug.Log(ResourcePath.LVLS + _jsonConfig.Map);
        GameObject map = Instantiate(Resources.Load<GameObject>(ResourcePath.LVLS + _jsonConfig.Map), _mapContainet.transform);
    }

    void StartIntro()
    {
        if (_coldStart)
            EndIntro();
        else
            CutScene.Instance.Show(Resources.Load<PlayableAsset>(ResourcePath.CUT_SCENE + _jsonConfig.CutScene), EndIntro);
    }
    void EndIntro()
    {
        _playerObj.SetActive(true);
        SpawnerEnemy.SetActive(true);
        RuntimeUI.Instance.ShowStartLogo();
    }
    
    #endregion


}
