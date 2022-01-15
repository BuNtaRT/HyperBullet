using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_start : MonoBehaviour
{
    /*
    // Don`t look this script please)), it`s very bad code, just this simple animation and camera set position on start game
    public Vector3 pos0;
    public Vector3 qua0;
    public Vector3 pos0to0;

    public Vector3 pos1;
    public Vector3 qua1;

    public Vector3 pos2;
    public Vector3 qua2;

    public Vector3 pos3;
    public Vector3 qua3;

    public Vector3 PosFinal;
    public Vector3 RotFinal;

    Transform camera;

    public GameObject Face;
    public GameObject Robot;

    public Material   matRobo;
    public GameObject Hero;
    public GameObject Sword;
    public GameObject Sphere;
    public GameObject Sphere2D;
    public GameObject Pistol;
    public GameObject Spawner;
    public GameObject StartText;
    public GameObject CentreHeroLight;

    public bool CoolStart = false;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("VirtualCamera").transform;
        if (CoolStart)
            StartCoroutine(final_positiontSet());
        else
            StartCoroutine(move0());
    }

    IEnumerator move0()
    {
        setCamera(pos0, qua0);
        float time;

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 1f;
            camera.position = Vector3.Lerp(pos0, pos0to0, timeStep);
            yield return null;
        }
        StartCoroutine(move1());
    }

    

    IEnumerator move1() 
    {
        //Time.timeScale = 0.54f;

        setCamera(pos1, qua1);
        Robot.SetActive(true);

        yield return new WaitForSeconds(0.75f);
        Robot.GetComponent<Animator>().speed = 0;
        yield return new WaitForSeconds(0.5f);

        SkinnedMeshRenderer renderer = Face.GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = renderer.materials;
        mats[0] = matRobo;
        renderer.materials = mats;
        yield return new WaitForSeconds(0.8f);

        Robot.GetComponent<Animator>().speed = 1;
        yield return new WaitForSeconds(0.25f);

        float time;
        float timeStep = 0f;
        Vector3 roboAfterRot = Robot.transform.eulerAngles;
        Vector3 roboAfterPos = Robot.transform.position;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.5f;
            Robot.transform.eulerAngles = Vector3.Lerp(roboAfterRot, new Vector3(0, 90, 0), timeStep);
            Robot.transform.position = Vector3.Lerp(roboAfterPos, new Vector3(-1f, 0, 0), timeStep);
            yield return null;
        }
        yield return new WaitForSeconds(0.15f);




        CentreHeroLight.SetActive(true);
        Sphere.SetActive(true);
        Sphere.GetComponent<Animation>().Play("Sphere3d");
        Sword.SetActive(true);
        Hero.SetActive(true);
        setCamera(pos2,qua2);
        Robot.SetActive(false);
        yield return new WaitForSeconds(1.8f);
        setCamera(pos3, qua3);

        yield return new WaitForSeconds(1.8f);
        StartCoroutine(final_positiontSet());


    }

    IEnumerator final_positiontSet() 
    {
        Hero.SetActive(true);
        Hero.GetComponent<Animator>().Play("shoot");

        setCamera(pos3, qua3);

        float time;
        float timeStep = 0f;
        Vector3 CameraAfterRot = camera.eulerAngles;
        Vector3 CameraAfterPos = camera.position;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 1f;
            camera.eulerAngles = Vector3.Lerp(CameraAfterRot, RotFinal, timeStep);
            camera.position = Vector3.Lerp(CameraAfterPos, PosFinal, timeStep);
            yield return null;
        }

        //setCamera(PosFinal,RotFinal);

        BlackScreenUI.Instance.Enable();
        yield return new WaitForSeconds(0.05f);
        StartText.SetActive(true);
        StartText.GetComponent<Animation>().Play("Start");
        Destroy(Robot);

        Sword.GetComponent<Animator>().SetInteger("StateSword",0);
        Hero.GetComponent<Animator>().SetInteger("HeroState",0);
        Sphere.SetActive(false);
        Sphere2D.SetActive(true);
        Pistol.GetComponent<Animator>().Play("PistolState");
        yield return new WaitForSeconds(0.15f);
        BlackScreenUI.Instance.Disable();
        Spawner.SetActive(true);
        yield return new WaitForSeconds(1f);

        StartText.GetComponent<Animation>().Play("StartOff");

        gameObject.GetComponent<CastSceneSwitcher>().NowGame_HideAll();

        Destroy(StartText,2f);  // убрать
        Destroy(this);
        


    }


    void setCamera(Vector3 pos, Vector3 rot) 
    {
        camera.position = pos;
        camera.eulerAngles = rot;
    }
    */
}
