using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public  GameObject Player;
    private string     _modifiedMethod = "";
    private int        _iClone;
    private float      _curretPrice;
    public static Shoot Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance obj over 1");
    }

    public void PLayerShoot(Vector3 positionShoot)
    {
        //TODO: Использовать списание пуль при выстреле 
        //if(SessionBullet.Instance.WriteOff(_curretPrice))
        for (int i = 0; i <= _iClone; i++)
        {
            if (i == 1)
                positionShoot *= -1; 

            Transform bullet = ObjPool.Instance.SpawnObj(TypeObj.Bullet, Vector3.up);
            Vector3 cleanCoordinate = new Vector3(positionShoot.x, 0, positionShoot.z);
            CameraShake.Instance.Shake(1f);

            if(i==0)
                Player.transform.DOLookAt(cleanCoordinate, 0.15f);

            cleanCoordinate = cleanCoordinate.normalized * 20;
            cleanCoordinate = new Vector3(cleanCoordinate.x, 1, cleanCoordinate.z);
            bullet.GetComponent<Bullet>().Init(cleanCoordinate);

            if (_modifiedMethod != "")
                Invoke(_modifiedMethod, 0);
        }
    }

    public void SetModified(string method) 
    {
        _modifiedMethod = method;
    }

    public void ClearModified() 
    {
        _modifiedMethod = "";
    }

    /*-----------------MODIFICATION-----------------*/
    private bool _slowMoPlay;
    private void SlowMo() 
    {
        if(!_slowMoPlay)
            StartCoroutine(SlowMotion());
    }
    private IEnumerator SlowMotion() 
    {
        _slowMoPlay = true;
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1f;

        _slowMoPlay = false;
    }

    public void PlayerClone1() 
    {
        Debug.LogWarning("this Working WHAT THIS DOING ????");
        _iClone = 1;
    }

    public void PlayerCloneDisable()
    {
        Debug.LogWarning("this Working WHAT THIS DOING ????");

        _iClone = 0;
        _modifiedMethod = "";
    }
}
