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
    private PlayableAsset _introScene;
    [SerializeField]
    private GameObject    _mapContainet;
    [SerializeField]
    private GameObject    _playerObj;

    public  GameObject    SpawnerEnemy ;
    public  SpawnSpell    SpawnerSpells;
    private bool          _coldStart   ;
    private ConfigLevel   _jsonConfig = null;

    [Serializable]
    private class ConfigLevel
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

    private void Start()
    {
        StartIntro();
    }

    #region Load Curret Spell
    private void LoadCurretSpell() 
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

    private void LoadEnemy() 
    {
        LoadRoboEnemy();
    }

    private void LoadRoboEnemy() 
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
    private void LoadMap()
    {
        Debug.Log(ResourcePath.LVLS + _jsonConfig.Map);
        GameObject map = Instantiate(Resources.Load<GameObject>(ResourcePath.LVLS + _jsonConfig.Map), _mapContainet.transform);
    }

    private void StartIntro()
    {
        if (_coldStart)
            EndIntro();
        else
            CutScene.Instance.Show(Resources.Load<PlayableAsset>(ResourcePath.CUT_SCENE + _jsonConfig.CutScene), EndIntro);
    }
    private void EndIntro()
    {
        _playerObj.SetActive(true);
        SpawnerEnemy.SetActive(true);
        RuntimeUI.Instance.ShowStartLogo();
    }
    #endregion
}
