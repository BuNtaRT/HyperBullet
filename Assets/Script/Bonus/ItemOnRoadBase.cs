using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnRoadBase : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer _ico;
    [SerializeField]
    private   SpriteRenderer _bg;
    private   Tween          _anim;
    protected TypeObj        _objectT;
    private   Color          _cB;
    private   Color          _cI;

    protected void InitAwake()
    {
        _cB = _bg.color;
        _cI = _ico.color;
        
    }

    protected void InitAnim() 
    {
        _bg.color  = _cB;
        _ico.color = _cI;
        _anim = _bg.DOColor(new Color(_cB.r, _cB.g, _cB.b, 0), 1f).SetLoops(3, LoopType.Yoyo).OnComplete(() =>
        {
            _ico.DOColor(new Color(_cI.r, _cI.g, _cI.b, 0), 0.5f).OnComplete(() =>
            {
                if (_objectT != TypeObj.None)
                    ObjPool.Instance.Destroy(_objectT, gameObject);
                else
                    gameObject.SetActive(false);
            });
        });
    }

    protected void End() 
    {
        _anim.Kill();
    }
}
