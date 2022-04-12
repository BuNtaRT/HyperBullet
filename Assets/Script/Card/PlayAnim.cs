using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    [SerializeField]
    private string   _nameAnimation;
    private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnValidate()
    {
        gameObject.GetComponent<Animator>().Play(_nameAnimation);
    }
}
