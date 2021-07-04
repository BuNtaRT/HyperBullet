using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapControll : MonoBehaviour
{
    public GameObject player;
    public SpawnerEnemy spEnemy;



    public Text Fails, GoodShoot;
    int fail, GS;

    public PerksUI perkCall;



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
                    GS++;
                    spEnemy.EnemyCount--;
                    player.transform.LookAt(hit.transform);
                    hit.transform.gameObject.GetComponent<EnemyAI>().Die();
                    CameraShake.singl.Shake(1f);

                }
                else if (tag == "Perk")
                {
                    perkCall.PerkShow();
                    hit.transform.gameObject.SetActive(false);
                }
                else if (tag == "Finish")
                {
                    fail++;
                }
                Fails.text = fail.ToString();
                GoodShoot.text = GS.ToString();
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
