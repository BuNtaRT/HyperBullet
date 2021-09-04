using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPointer : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Transform _poinet;

    public void LookAt(Vector3 dot) 
    {
        if(_spriteRenderer.enabled == false)
            _spriteRenderer.enabled = true;
        Vector3 clearCoordinate = dot.normalized;
        float rot_z = Mathf.Atan2(clearCoordinate.x, clearCoordinate.z) * Mathf.Rad2Deg;
        _poinet.rotation = Quaternion.Euler(90f, -45, -rot_z);
    }

    public void End(Vector3 dot) 
    {
        _spriteRenderer.enabled = false;
        
    }
}
