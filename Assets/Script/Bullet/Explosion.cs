using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {

    }
    private void OnEnable()
    {
                Color spC = gameObject.GetComponent<SpriteRenderer>().color;
        Sequence sequence = DOTween.Sequence();
        gameObject.transform.localScale = Vector3.zero;
        sequence.Append(gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(spC.r, spC.g, spC.b, 0.3f),0.4f));
        sequence.Append(gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(spC.r, spC.g, spC.b, 0.8f),0.2f));
        sequence.Append(gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(spC.r, spC.g, spC.b, 0),0.4f).OnComplete(() => { Die(); }));
        gameObject.transform.DOPunchScale(Vector3.one*10,1f,1,0.5f);
    }
    void Die() 
    {
        ObjPool.Instance.Destroy(TypeObj.ExplosionFromBullet,gameObject);
    }
}
