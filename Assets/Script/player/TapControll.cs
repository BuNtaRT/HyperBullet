using System;
using UnityEngine;
using UnityEngine.UI;

public class TapControll : MonoBehaviour
{
    public  SpawnerEnemy    SpawnerEnemy         ;
    public  SpellPointer    SpellPointer         ;
    private bool            _disable      = false;
    private Shoot           _shoot               ;
    private Vector2         _beganTouch          ;
    private float           _resolCoeff          ;
    private bool            _reqNextPoint = false;
    private Action<Vector3> _nextPointFor        ;

    public static TapControll Instance;

    private void Awake()
    {
        if (Instance != null)
            throw new System.Exception("TapControll install over 1");
        else
            Instance = this;

        //8.5 radius sphere on fullHD
        _resolCoeff = 8.5f*((float)1080 / Screen.width);

        GlobalEventsManager.OnPause.AddListener(PauseSub);
    }

    private bool _pause = false;
    private void PauseSub(PauseStatus status, bool enable) 
    {
        if(status != PauseStatus.pickSpellDir)
            _pause = enable;
    }

    private void Start()
    {
        _shoot = Shoot.Instance;
    }

    private void Update()
    {
        if (!_pause)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    if (_reqNextPoint)
                    {
                        GivePoint(hit.point);
                    }
                    else
                    {
                        string tag = hit.transform.tag;
                        if (tag == "ItemOnRoad")
                            hit.transform.GetComponent<IItemOnRoad>().Pick();
                        else
                            _shoot.PLayerShoot(hit.point);
                    }
                }
            }
#endif
            if (Input.touchCount == 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    if (_reqNextPoint)
                    {
                        GivePoint(hit.point);
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        Time.timeScale = 0.2f;
                        if (hit.point.sqrMagnitude <= _resolCoeff)
                            SpellPointer.LookAt(hit.point, true);
                        else
                            SpellPointer.LookAt(hit.point, false);
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        _beganTouch = new Vector2(hit.point.x, hit.point.z);
                        string tag = hit.transform.tag;
                        if (tag == "ItemOnRoad")
                            hit.transform.GetComponent<IItemOnRoad>().Pick();
                        else
                            _shoot.PLayerShoot(hit.point);
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        Vector2 endPos = new Vector2(hit.point.x, hit.point.z);
                        Time.timeScale = 1f;
                        if ((_beganTouch - endPos).sqrMagnitude >= _resolCoeff)
                            SpellPointer.End(true);
                        else
                            SpellPointer.End(false);
                    }
                }
            }
        }
    }

    public void GetNextPoint(Action<Vector3> @for) 
    {
        GlobalEventsManager.InvokPause(PauseStatus.pickSpellDir,true);
        RuntimeUI.Instance.ShowMessage("Выбите направление");
        _nextPointFor = @for;
        _reqNextPoint = true;
    }

    private void GivePoint(Vector3 point) 
    {
        GlobalEventsManager.InvokPause(PauseStatus.pickSpellDir, false);
        _reqNextPoint = false;
        _nextPointFor?.Invoke(point);
    }
}
