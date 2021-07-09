using UnityEngine;
public class TapControll : MonoBehaviour
{
    public GameObject   Player;
    public SpawnerEnemy SpawnerEnemy;
    public PerksUI      PerkCall;

    bool an = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                string tag = hit.transform.tag;
                //if (tag == "enemy")
                //{
                //    //SpawnerEnemy.MinusEnemy();
                //    //Player.transform.LookAt(hit.transform);
                //    Debug.Log(hit.transform.position);
                //    //Shoot(hit.transform);
                //    //hit.transform.gameObject.GetComponent<EnemyAIBase>().Die();
                //    //CameraShake.singl.Shake(1f);

                //}
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
            Vector3 cleanCoordinate = new Vector3(positionShoot.x, 0, positionShoot.z);
            CameraShake.singl.Shake(1f);
            Player.transform.LookAt(cleanCoordinate);
            Transform bullet = ObjPool.Instance.SpawnObj(TypeObj.Bullet, Vector3.up);

            cleanCoordinate = cleanCoordinate.normalized * 20;
            cleanCoordinate = new Vector3(cleanCoordinate.x, 1, cleanCoordinate.z);
            bullet.GetComponent<Bullet>().Init(cleanCoordinate);
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
}
