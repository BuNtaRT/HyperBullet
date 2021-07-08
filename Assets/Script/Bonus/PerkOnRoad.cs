using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkOnRoad : MonoBehaviour
{
    [SerializeField] SpriteRenderer _bgPerk;

    void Start()
    {
        _bgPerk = transform.Find("bg").GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(alphaNotification());
    }

    IEnumerator alphaNotification() 
    {
        yield return new WaitForSeconds(1.2f);
        float time = 0.5f;
        float timeStep = 0f;
        Color start = _bgPerk.color;
        Color end = new Color(start.r, start.g, start.b, 0);

        for (int i = 0; i <= 3; i++)
        {
            while (timeStep < 1.0f)
            {
                timeStep += Time.deltaTime / time;
                _bgPerk.color = Color.Lerp(end, start, Mathf.PingPong(timeStep, 1));
                yield return null;
            }
            timeStep = 0;
        }
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / time;
            _bgPerk.color = Color.Lerp(start, end, timeStep);
            yield return null;
        }

        gameObject.SetActive(false);

    }

}
