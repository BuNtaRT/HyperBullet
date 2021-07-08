using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeObj : byte
{
    Card,
    Enemy,
}

public class ObjPool : MonoBehaviour
{
    public static ObjPool Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance obj over 1");
    }

    [Serializable]
    public struct ObjectsInfo 
    {
        public TypeObj    Type;
        public GameObject Prefab;
        public int        Count;
    }

    [SerializeField]
    List<ObjectsInfo> _objectsInfo = new List<ObjectsInfo>();

    Dictionary<TypeObj, Queue<GameObject>> poolDictionary;
    void Start()
    {
        poolDictionary = new Dictionary<TypeObj, Queue<GameObject>>();

        foreach (ObjectsInfo temp in _objectsInfo)
        {
            Queue<GameObject> tempQueue = new Queue<GameObject>();
            for (int i = 0; i < temp.Count; i++)
            {
                GameObject obj = Instantiate(temp.Prefab);
                obj.SetActive(false);
                tempQueue.Enqueue(obj);
            }
            poolDictionary.Add(temp.Type, tempQueue);
        }
    }

    public Transform SpawnObj(TypeObj type, Vector3 position) 
    {
        GameObject temp = poolDictionary[type].Dequeue();
        temp.SetActive(true);
        temp.transform.position = position;
        poolDictionary[type].Enqueue(temp);
        return temp.transform;
    }

    

}
