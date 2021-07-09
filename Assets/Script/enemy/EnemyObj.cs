﻿using DG.Tweening;
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

    EnemyAIBase    _currEnemyAI;

    SpawnerEnemy _spawnerEnemy;
    public void ReInit(EnemyAIBase enemyAIBase) 
    {
        SphereController.Sphere.RemoveEnemy(this);
        _anim.Play("Run");
        _anim.SetBool("Run", true);
        _currEnemyAI         = enemyAIBase;
        _boxCollider.enabled = true;
        SpeedAnim = 1;
    }

    private void Awake()
    {
        _true_light  = transform.Find("Light").GetComponent<Light>();
        _fake_light  = transform.Find("FackeLight").GetComponent<SpriteRenderer>();
        _helmet      = transform.Find("Face.lod2").gameObject;
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _anim        = gameObject.GetComponent<Animator>();
        _true_light.gameObject.SetActive(false);
    }


    private void Start()
    {
        _spawnerEnemy = SpawnerEnemy.Instance;
        ShowCastScene(false);
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
            MoveSpeed = MoveSpeed / slowX;
        }
    }

    public void Die()
    {
        _spawnerEnemy.MinusEnemy();
        _boxCollider.enabled = false;
        SphereController.Sphere.RemoveEnemy(this);
        Destroy(_currEnemyAI);
        // set reverse position
        Vector3 tempRot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(tempRot.x, tempRot.y + 180, tempRot.z);
        GameObject tempDot = new GameObject();
        tempDot.transform.SetParent(transform);
        tempDot.transform.localPosition = new Vector3(0, -1.5f, Random.Range(0.5f, 2.2f));
        Vector3 roboBeforePos = tempDot.transform.position;
        transform.eulerAngles = tempRot;
        Destroy(tempDot);

        ChanceBonus.Instance.EnemyDie(transform.position);
        _anim.Play("Die");
        _anim.SetBool("Die", true);
        gameObject.transform.DOMove(roboBeforePos, 0.8f);
        Color lightColor = _fake_light.color;
        _fake_light.DOColor(new Color(lightColor.r, lightColor.g, lightColor.b, 0), 1);
        StartCoroutine(SetActiveFalse());
    }
    IEnumerator SetActiveFalse()
    {
        yield return new WaitForSeconds(1.1f);
        gameObject.SetActive(false);
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
}