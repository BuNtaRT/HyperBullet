using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prediction : MonoBehaviour, IPerk
{
    private Queue<Transform> _pointers = new Queue<Transform>();

    public void InitPerk()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>(ResourcePath.PERK_OBJ + "Pointer"));
            temp.SetActive(false);
            _pointers.Enqueue(temp.transform);
        }
        SpawnerEnemy.Instance.OnSpawnEnemy.AddListener(OnSpawnEnemy);
    }

    public void DestroyPerk()
    {
        foreach (var item in _pointers)
        {
            Destroy(item.gameObject);
        }
        SpawnerEnemy.Instance.OnSpawnEnemy.RemoveListener(OnSpawnEnemy);
        Destroy(gameObject);
    }

    private void OnSpawnEnemy(Transform enemy) 
    {
        Transform temp = _pointers.Dequeue();
        if (!temp.gameObject.activeSelf)
        {
            temp.gameObject.SetActive(true);
            temp.LookAt(enemy);
            temp.eulerAngles = new Vector3(90, temp.eulerAngles.y, temp.eulerAngles.z);
            Color color = new Color(0, 1, 0.89f, 0);
            temp.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
            temp.GetComponent<SpriteRenderer>().DOColor(color, 0.5f).OnComplete(() =>
            { temp.gameObject.SetActive(false); });
        }
        _pointers.Enqueue(temp);
    }
}
