using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    [SerializeField] string nameAnimation;
    Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Debug.Log(gameObject.name);
    }

    private void OnValidate()
    {
        gameObject.GetComponent<Animator>().Play(nameAnimation);
        Debug.Log("PLay anim");
    }
}
