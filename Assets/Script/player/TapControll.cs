using DG.Tweening;
using UnityEngine;
public class TapControll : MonoBehaviour
{
    public GameObject   Player;
    public SpawnerEnemy SpawnerEnemy;
    public PerksUI      PerkCall;
    bool                _disable     = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_disable)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                string tag = hit.transform.tag;
                if (tag == "Perk")
                {
                    PerkCall.PerkShow();
                    hit.transform.gameObject.SetActive(false);
                }
                else 
                {
                    Shoot(hit.point);
                }
            }
        }

        void Shoot(Vector3 positionShoot)
        {
            Transform bullet = ObjPool.Instance.SpawnObj(TypeObj.Bullet, Vector3.up);
            if (bullet != null)
            {
                Vector3 cleanCoordinate = new Vector3(positionShoot.x, 0, positionShoot.z);
                CameraShake.Instance.Shake(1f);
                Player.transform.DOLookAt(cleanCoordinate, 0.15f);


                cleanCoordinate = cleanCoordinate.normalized * 20;
                cleanCoordinate = new Vector3(cleanCoordinate.x, 1, cleanCoordinate.z);
                bullet.GetComponent<Bullet>().Init(cleanCoordinate);
            }
        }

        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) 
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    RaycastHit hit = new RaycastHit();

        //    if (Physics.Raycast(ray.origin, ray.direction, out hit))
        //    {
        //        string tag= hit.transform.tag;
        //        if (tag == "enemy")
        //        {
        //            GS++;
        //            spEnemy.EnemyCount--;
        //            player.transform.LookAt(hit.transform);
        //            hit.transform.gameObject.GetComponent<EnemyAI>().Die();
        //            CameraShake.Instance.Shake(1f);

        //        }
        //        else if (tag == "Finish")
        //        {
        //            fail++;
        //        }
        //        else if (tag == "Perk") 
        //        {
        //            perkCall.PerkGet();
        //            Destroy(hit.transform.gameObject);
        //        }
        //        Fails.text = fail.ToString();
        //        GoodShoot.text = GS.ToString();
        //    }
        //}
    }

    public void Disable(bool dis)
    {
        _disable = dis;
    }
}
