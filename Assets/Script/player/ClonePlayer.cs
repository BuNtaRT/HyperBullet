using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform _clone1;
    [SerializeField]
    private Transform _clone2;

    public static ClonePlayer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance obj over 1");
    }

    public void ClonePerkEnable() 
    {
        _player.position = new Vector3(_player.position.x,_player.position.y,0.35f);
        _clone1.gameObject.SetActive(true);
    }

    public void ClonePerkDisable()
    {
        _player.position = new Vector3(_player.position.x, _player.position.y, 0);
        _clone1.gameObject.SetActive(false);
    }
}
