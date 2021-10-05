using UnityEngine;
using UnityEngine.UI;

public class TapControll : MonoBehaviour
{
    public SpawnerEnemy SpawnerEnemy;
    public SpellPointer SpellPointer;
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

        if (Input.touchCount == 1 && !_disable)
        {


            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    Time.timeScale = 0.2f;
                    if (hit.point.sqrMagnitude <= 10)
                        SpellPointer.LookAt(hit.point,true);
                    else
                        SpellPointer.LookAt(hit.point,false);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    Time.timeScale = 1f;
                    SpellPointer.End(hit.point);
                }
            }
            
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

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
        }

    }

    public void Disable(bool dis)
    {
        _disable = dis;
    }
}
