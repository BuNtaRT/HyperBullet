using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public class LoadLvl : MonoBehaviour
{
    struct EnemyLoad
    {
        public string Type;
        public int Count;
    }

    List<EnemyLoad> _enemyList = new List<EnemyLoad>();
    public SpawnerEnemy SpawnerEnemy;

    private void Awake()
    {
        PlayerPrefs.SetInt(PlayerPKey.LVL, 0);
        XmlDocument loadXml = new XmlDocument();
        TextAsset xmlFile = Resources.Load<TextAsset>(ResourcePath.LVL_CONFIG + PlayerPrefs.GetInt(PlayerPKey.LVL));
        loadXml.LoadXml(xmlFile.text);
        XmlElement rootXml = loadXml.DocumentElement;
        foreach (XmlNode temp in rootXml) 
        {
            EnemyLoad tempEnemy = new EnemyLoad();
            if (temp.Attributes.Count > 0) 
            {
                tempEnemy.Type = temp.Attributes.GetNamedItem("type").Value;
            }

            tempEnemy.Count =  XmlConvert.ToInt32(temp.SelectSingleNode("count").InnerText);
            if(tempEnemy.Count!=0)
                _enemyList.Add(tempEnemy);
        }
        SendToSpawner();
    }

    void SendToSpawner() 
    {
        foreach (EnemyLoad tempLoad in _enemyList) 
        {
            GameObject tempObjBeh = Instantiate(Resources.Load<GameObject>(ResourcePath.ENEMY_BEHAIVOR + tempLoad.Type));
            EnemyAIBase tempeEnemyAI = tempObjBeh.GetComponent<EnemyAIBase>();
            SpawnerEnemy.AddNewBehaivor(tempeEnemyAI,tempLoad.Count);
            //Destroy(tempObjBeh);
        }
    }
}
