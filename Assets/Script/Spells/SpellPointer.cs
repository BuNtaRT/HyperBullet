using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpellPointer : MonoBehaviour
{
    //Controll Spell pointer (Oh realy??)
    [SerializeField] SpellCells     _spellCells;
    [SerializeField] SpriteRenderer _pointerSp;
    [SerializeField] Transform      _poinet;
                     bool           _enablePointer = false;
                     bool           _hightStatus   = false;

    public void LookAt(Vector3 dot,bool close) 
    {


        if (dot == Vector3.zero) // if input zero, because we brake pick
        {
            _spellCells.DontPick();
        }

        if (close)
            Hide(true);
        else
            Hide(false);

        if (_enablePointer == false)
        {
            StartUseCells();
        }

        float rot_z = CalcDeg(dot);
        _poinet.rotation = Quaternion.Euler(90f, -45, -rot_z);

        _spellCells.LookTo(rot_z);
    }

    public void End(Vector3 dot) 
    {

        //_cellsManeger.Pick(CalcDeg(dot));
        if (!_hightStatus)
            _spellCells.Pick();
        else
            _spellCells.DontPick();
        
        _enablePointer = false;
        ShowPointer(false);
        _spellCells.SetSaturation(0.3f);
    }
    public void Hide(bool hide) 
    {
        if (_hightStatus != hide)
        {
            _spellCells.Hide(hide);
            ShowPointer(!hide);
            _hightStatus = hide;
        }
    }

    void StartUseCells()
    {
        _hightStatus = false;
        _enablePointer = true;
        _spellCells.SetSaturation(1f);
        ShowPointer(true);
        _spellCells.Reinit();
    }

    void ShowPointer(bool enable) 
    {
        //_enablePointer = enable;
        float alpha = enable ? 1 : 0;
        _pointerSp.DOColor(new Color(_pointerSp.color.r, _pointerSp.color.g, _pointerSp.color.b, alpha),0.6f).SetUpdate(true);
        _pointerSp.enabled = true;
    }

    float CalcDeg(Vector3 dot) 
    {
        Vector3 clearCoordinate = dot.normalized;
        return Mathf.Atan2(clearCoordinate.x, clearCoordinate.z) * Mathf.Rad2Deg;
    }
}
