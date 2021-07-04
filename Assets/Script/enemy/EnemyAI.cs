using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform GoTo;
    public float MoveSpeed;
    public bool Dnager = true;
    public Material MatDanger;
    public List<SkinnedMeshRenderer> LodsMesh;

    Animator Anim;


    // For CastScene
    [SerializeField] Light True_light;
    [SerializeField] SpriteRenderer Fake_light;
    [SerializeField] GameObject Helmet;

    int modify = 1;
    private void Awake()
    {
        
    }

    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
        Anim.Play("Run");
        transform.LookAt(GoTo);


        Fake_light = gameObject.transform.Find("FackeLight").GetComponent<SpriteRenderer>();
        True_light = gameObject.transform.Find("Light").GetComponent<Light>();
        Helmet = gameObject.transform.Find("Face.lod1").gameObject;

        if (Dnager)
        {

            foreach (SkinnedMeshRenderer renderer in LodsMesh) 
            {
                Material[] mats = renderer.materials;
                mats[0] = MatDanger;
                renderer.materials = mats;
            }
            True_light.color = new Color(1, 0, 0.390801f);
            Fake_light.color = new Color(1, 0, 0.390801f,0.4f);
        }
        else 
        {
            Fake_light.color = new Color(0, 1, 0.9764706f, 0.4f);
            True_light.color = new Color(0, 1, 0.9764706f);
        }
        True_light.gameObject.SetActive(false);
        ShowCastScene(false);
        Anim.SetBool("Run", true);
        
        StartCoroutine(Go());
    }

    public void ShowCastScene(bool cast) 
    {
        True_light.enabled =cast;
        Fake_light.enabled = !cast;
        if (cast)
            Helmet.layer = 8;
        else
            Helmet.layer = 9;
    }

    public void SlowModify(int slowX) 
    {
        modify = slowX;

    }

    IEnumerator Go() 
    {
        float distance = Vector3.Distance(gameObject.transform.position,GoTo.position);
        float time = distance/MoveSpeed;
        float timeStep = 0f;
        Vector3 roboAfterPos = gameObject.transform.position;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / time / modify;
            gameObject.transform.position = Vector3.Lerp(roboAfterPos, GoTo.position, timeStep);
            yield return null;
        }

        yield return new WaitForSeconds(0);
        
    }

    public void Die() 
    {
        SphereController.Sphere.RemoveEnemy(this);
        StopAllCoroutines();
        StartCoroutine(DieAnimBack());
        gameObject.GetComponent<BoxCollider>().enabled = false;
        ChanceBonus.singl.EnemyDie(transform.position);
        Destroy(gameObject, 1.5f);
        Anim.Play("Die");
        Anim.SetBool("Die", true);
    }



    IEnumerator DieAnimBack() 
    {

        Vector3 tempRot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(tempRot.x, tempRot.y + 180, tempRot.z);
        GameObject tempDot = new GameObject();
        tempDot.transform.SetParent(transform);
        tempDot.transform.localPosition = new Vector3(0,-1.5f,Random.Range(0.5f,2.2f));
        Vector3 roboBeforePos = tempDot.transform.position;
        transform.eulerAngles = tempRot;
        Destroy(tempDot);




        float time = 1f;
        float timeStep = 0f;
        Vector3 roboAfterPos = gameObject.transform.position;
       

        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / time;
            gameObject.transform.position = Vector3.Lerp(roboAfterPos, roboBeforePos, timeStep);
            yield return null;
        }
    }


    public void Attack() 
    {
        Anim.SetBool("Run", false);
    }


    public void SlowTimeEnable(bool enable) 
    {
       
        if (enable)
        {
            modify = 4;
            Anim.SetFloat("SpeedAnim", 0.25f);
        }
        else 
        {
            modify = 1;
            Anim.SetFloat("SpeedAnim", 1);
        }

    }

}
