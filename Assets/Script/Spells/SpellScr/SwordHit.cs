using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : SpellBase
{
    Transform _wave;
    float speed;
    float distance;

    protected override void LvlSpellInit() 
    {
        switch (_spell.Lvl)
        {
            case 1:
                speed = 1;
                distance = 5;
                break;
            case 2:
                speed = 1.2f;
                distance = 10;
                break;
            case 3:
                speed = 1.8f;
                distance = 13;
                break;
            default:
                break;
        }
    }

    protected override void InitOverride()
    {
        base.InitOverride();
        CutScene.Instance.Show(_spell.Animation, EndAnimation);
    }

    protected override void EndAnimation()
    {
        base.EndAnimation();
        _wave = Instantiate(Resources.Load<GameObject>(ResourcePath.SPELL_RES + "Wave").transform);
        LookAtProp(_wave);
        SpriteRenderer waveSp = _wave.GetChild(0).GetComponent<SpriteRenderer>();
        Sequence sequence = DOTween.Sequence().OnComplete(() => Destroy());
        sequence.Insert(0,_wave.DOMove(_touchPoint.normalized * distance, 2/speed));
        sequence.Insert(0, _wave.DOScale(new Vector3(3.5f, 3.5f, 3.5f), 2/speed));
        sequence.Insert(1.6f/speed, waveSp.DOColor(new Color(waveSp.color.r, waveSp.color.g, waveSp.color.b,0), 0.4f/speed));
    }

    void Destroy() 
    {
        Destroy(_wave.gameObject);
        Destroy(gameObject);
    }

}
