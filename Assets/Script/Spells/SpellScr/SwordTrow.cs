using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrow : SpellBase
{

    [SerializeField]
    Transform _swordSp;
    [SerializeField]
    //слуд в какую сторону летит меч
    Transform _trace;
    protected override void InitOverride()
    {
        CutScene.Instance.Show(_spell.Animation,EndAnimation);
    }

    protected override void EndAnimation() 
    {
        base.EndAnimation();
        _trace = Instantiate(Resources.Load<GameObject>(ResourcePath.SPELL_RES + "Trace"),_mapContainer).transform;

        LookAtProp(_trace);

        _swordSp = Instantiate(Resources.Load<GameObject>(ResourcePath.SPELL_RES + "SwordMiniSp")).transform;
        _swordSp.gameObject.SetActive(false);
        StartCoroutine(SwordAnim());
    }

    IEnumerator SwordAnim() 
    {
        SpriteRenderer spriteTrace = _trace.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Color tC = spriteTrace.color;
        spriteTrace.color = new Color(tC.r, tC.g, tC.b, 0);
        spriteTrace.DOColor(tC,0.4f);

        yield return new WaitForSeconds(0.2f);

        _swordSp.gameObject.SetActive(true);
        Vector3 finalPoint = _touchPoint.normalized * 20;
        _swordSp.DOMove(finalPoint,2f);
        _swordSp.DORotate(new Vector3(0,1080,0),2,RotateMode.FastBeyond360);
        yield return new WaitForSeconds(0.5f);
        spriteTrace.DOColor(new Color(tC.r, tC.g, tC.b, 0), 0.2f);
        yield return new WaitForSeconds(1.7f);  
        Destroy();
    }

    void Destroy() 
    {
        Destroy(_swordSp.gameObject);
        Destroy(_trace.gameObject);
        Destroy(gameObject);
    }


}
