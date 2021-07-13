using UnityEngine;
public class TapControll : MonoBehaviour
{

    public SpawnerEnemy SpawnerEnemy;
    public PerksUI      PerkCall;
    bool                _disable     = false;
    Shoot               _shoot;
    private void Start()
    {
        _shoot = Shoot.Instance;
    }

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
                    _shoot.PLayerShoot(hit.point);
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

    public void Disable(bool dis)
    {
        _disable = dis;
    }
}
