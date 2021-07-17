using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnEnable()
    {
        //transform.localScale = Vector3.zero;
        //transform.DOPunchScale(Vector3.one * 10, 1f, 1, 0.5f).OnComplete(() => { Die(); });
        StartCoroutine(Die());
    }
    IEnumerator Die() 
    {
        yield return new WaitForSeconds(1f);
        ObjPool.Instance.Destroy(TypeObj.ExplosionFromBullet,gameObject);
    }
}
