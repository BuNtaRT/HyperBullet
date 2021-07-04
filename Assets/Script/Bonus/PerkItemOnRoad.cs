using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkItemOnRoad : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer BgPerk;


    void Start()
    {
        BgPerk = transform.Find("bg").GetComponent<SpriteRenderer>();
    }

    void StartDestroy() 
    {
        StartCoroutine(AlphaNotification());
    }

    private void OnEnable()
    {
        StartCoroutine(AlphaNotification());
    }


    IEnumerator AlphaNotification() 
    {
        yield return new WaitForSeconds(1.2f);
        float time = 0.5f;
        float timeStep = 0f;
        Color Start = BgPerk.color;
        Color End = new Color(Start.r, Start.g, Start.b, 0);

        for (int i = 0; i <= 3; i++)
        {
            while (timeStep < 1.0f)
            {
                timeStep += Time.deltaTime / time;
                BgPerk.color = Color.Lerp(End, Start, Mathf.PingPong(timeStep, 1));
                yield return null;
            }
            timeStep = 0;
        }
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / time;
            BgPerk.color = Color.Lerp(Start, End, timeStep);
            yield return null;
        }

        gameObject.SetActive(false);

    }

}
