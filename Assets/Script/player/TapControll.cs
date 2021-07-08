using UnityEngine;

public class TapControll : MonoBehaviour
{
    public GameObject   Player;
    public SpawnerEnemy SpawnerEnemy;
    public PerksUI      PerkCall;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                string tag = hit.transform.tag;
                if (tag == "enemy")
                {
                    SpawnerEnemy.MinusEnemy();
                    Player.transform.LookAt(hit.transform);
                    hit.transform.gameObject.GetComponent<EnemyAIBase>().Die();
                    CameraShake.singl.Shake(1f);

                }
                else if (tag == "Perk")
                {
                    PerkCall.PerkShow();
                    hit.transform.gameObject.SetActive(false);
                }
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
}
