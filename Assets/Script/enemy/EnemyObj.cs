using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObj : MonoBehaviour
{
    public float SpeedAnim { get; private set; }
    public float MoveSpeed { get; private set; }
    Animator       _anim;
    BoxCollider    _boxCollider;
    GameObject     _helmet;        // For CastScene
    Light          _true_light;
    SpriteRenderer _fake_light;
    GameObject     _lod_4;

    EnemyAIBase    _currEnemyAI;

    public void ReInit(EnemyAIBase enemyAIBase) 
    {
        _anim.Play("Run");
        _anim.SetBool("Run", true);
        _currEnemyAI         = enemyAIBase;
        _boxCollider.enabled = true;
        _anim.enabled        = true;
        SpeedAnim            = 1;

        ChangeLayer((int)ObjLayer.PropLight);

    }

    private void Awake()
    {
        _true_light  = transform.Find("Light").GetComponent<Light>();
        _fake_light  = transform.Find("FackeLight").GetComponent<SpriteRenderer>();
        _helmet      = transform.Find("Face.lod2").gameObject;
        _lod_4       = transform.Find("BH_2_lod4").gameObject;
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _anim        = gameObject.GetComponent<Animator>();
        _true_light.gameObject.SetActive(false);
    }


    private void Start()
    {
        //ShowCastScene(false);
    }

    public void SetColor(Color color) 
    {
        _fake_light.color = color;
        _true_light.color = color;
    }
    public void SetSpeed(float speed) 
    {
        MoveSpeed = speed;
    }

    public void ShowCastScene(bool cast)
    {
        _true_light.enabled = cast;
        _fake_light.enabled = !cast;
        if (cast)
            _helmet.layer = 8;
        else
            _helmet.layer = 9;
    }

    float _defaultSpeed;
    void SlowSpeedModify(float slowX)
    {
        if (slowX == 0)
        {
            MoveSpeed = _defaultSpeed;
        }
        else
        {
            _defaultSpeed = MoveSpeed;
            MoveSpeed     = MoveSpeed / slowX;
        }
    }

    public void Die()
    {
        GlobalEventsManager.SendEnemyKill(transform);
        _boxCollider.enabled = false;
        //Destroy(_currEnemyAI);
        //if (_currEnemyAI != null) 
        //{
        //    Debug.LogError(_currEnemyAI + "Not destroy");
        //}
        // set reverse position
        Vector3 tempRot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(tempRot.x, tempRot.y + 180, tempRot.z);
        GameObject tempDot = new GameObject();
        tempDot.transform.SetParent(transform);
        tempDot.transform.localPosition = new Vector3(0, -1.5f, Random.Range(0.5f, 2.2f));
        Vector3 roboBeforePos = tempDot.transform.position;
        transform.eulerAngles = tempRot;
        Destroy(tempDot);

        _anim.Play("Die");
        _anim.SetBool("Die", true);
        gameObject.transform.DOMove(roboBeforePos, 0.8f);
        Color lightColor = _fake_light.color;
        _fake_light.DOColor(new Color(lightColor.r, lightColor.g, lightColor.b, 0), 1);
        StartCoroutine(SetActiveFalse());

        Destroy(_currEnemyAI);
    }
    IEnumerator SetActiveFalse()
    {
        yield return new WaitForSeconds(1.1f);
        ObjPool.Instance.Destroy(TypeObj.Enemy,gameObject);
    }
    public void Attack()
    {
        _anim.SetBool("Run", false);
    }

    /// <summary>
    /// Applay slowMo effect to enemy 
    /// </summary>
    /// <param name="enable">true = enable, false = disable </param>
    public void SlowTimeEnable(bool enable)
    {
        if (enable)
        {
            _currEnemyAI.InSphere = true;
            SpeedAnim = 4;
            _anim.SetFloat("SpeedAnim", 0.25f);
            SlowSpeedModify(4);
        }
        else
        {
            SpeedAnim = 1;
            _anim.SetFloat("SpeedAnim", 1);
            SlowSpeedModify(0);
        }
        
    }

    public void InSphere() 
    {
        _currEnemyAI.InSphere = true; // Disable Dodge in enemy smart
    }

    // for Shadowenemy 
    public void ChangeLayer(int layer) 
    {
        _lod_4.layer = layer;
        _fake_light.gameObject.layer = layer;
        _helmet.layer = layer;
    }
}
