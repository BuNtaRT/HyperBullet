using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPointer : MonoBehaviour
{
    //Controll Spell pointer (Oh realy??)
    [SerializeField] SpellCells     _cellsManeger;
    [SerializeField] SpriteRenderer _pointerSp;
    [SerializeField] Transform      _poinet;

    public void LookAt(Vector3 dot) 
    {
        if (dot == Vector3.zero) // if input zero, because we brake pick
        {
            _cellsManeger.DontPick();
        }

        if (_pointerSp.enabled == false)
        {
            StartUseCells();
        }

        float rot_z = CalcDeg(dot);
        _poinet.rotation = Quaternion.Euler(90f, -45, -rot_z);

        _cellsManeger.LookTo(rot_z);
    }

    public void End(Vector3 dot) 
    {
        _cellsManeger.Pick(CalcDeg(dot));

        _pointerSp.enabled = false;
        _cellsManeger.SetSaturation(0.3f);
    }

    void StartUseCells()
    {
        _cellsManeger.SetSaturation(1f);    
        _pointerSp.enabled = true;
        _cellsManeger.Reinit();
    }

    float CalcDeg(Vector3 dot) 
    {
        Vector3 clearCoordinate = dot.normalized;
        return Mathf.Atan2(clearCoordinate.x, clearCoordinate.z) * Mathf.Rad2Deg;
    }
}
