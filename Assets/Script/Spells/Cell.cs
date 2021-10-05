using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    SpriteRenderer _sprite;
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    public void Activate() 
    {
        StartCoroutine(nameof(OffSp));

    }

    public bool Activ() 
    {
        return _sprite.enabled;
    }

    public void SetSpell() 
    {
        
    }

    IEnumerator OffSp() 
    {
        yield return new WaitForSecondsRealtime(1f);
        _sprite.enabled = false;
    }


}
