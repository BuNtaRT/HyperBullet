using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpellPointer : MonoBehaviour
{
    //Management graphic pointer and send deg to Cell maneger
    //Controll Spell pointer (Oh realy??)
    [SerializeField]
    private SpellCells     _spellCells;
    [SerializeField]
    private SpriteRenderer _pointerSp;
    [SerializeField]
    private Transform      _poinet;
    private bool           _enablePointer = false;
    private bool           _hightStatus   = false;
    private bool           _closeLast     = true ;

    public void LookAt(Vector3 dot,bool close) 
    {
        if (_closeLast != close)
        {
            _closeLast = close;
            if (close)
            {
                _spellCells.StandByState(true);
                ShowPointer(false);
            }
            else
            {
                _spellCells.StandByState(false);
                ShowPointer(true);
            }
        }

        if (_enablePointer == false)
        {
            StartUseCells();
        }

        float rot_z = CalcDeg(dot);
        _poinet.rotation = Quaternion.Euler(90f, -45, -rot_z);

        _spellCells.LookTo(rot_z);
    }

    public void End(bool pick) 
    {
        _spellCells.EndState(pick);

        _enablePointer = false;
        ShowPointer(false);
    }

    private void StartUseCells()
    {
        _closeLast = true;
        _enablePointer = true;
        _spellCells.StartState();
        ShowPointer(true);
    }

    private void ShowPointer(bool enable) 
    {

            float alpha = enable ? 1 : 0;
            _pointerSp.DOColor(new Color(_pointerSp.color.r, _pointerSp.color.g, _pointerSp.color.b, alpha), 0.6f).SetUpdate(true);
            _pointerSp.enabled = true;
            _hightStatus = enable;
    }

    private float CalcDeg(Vector3 dot) 
    {
        Vector3 clearCoordinate = dot.normalized;
        return Mathf.Atan2(clearCoordinate.x, clearCoordinate.z) * Mathf.Rad2Deg;
    }
}
