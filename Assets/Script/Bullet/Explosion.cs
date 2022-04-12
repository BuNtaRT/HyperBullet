using UnityEngine;
//Expl from bullet, "OnDestroyExpl" - call from animation event 
public class Explosion : MonoBehaviour
{
    private void OnDestroyExpl() 
    {
        ObjPool.Instance.Destroy(TypeObj.ExplosionFromBullet,gameObject);
    }
}
