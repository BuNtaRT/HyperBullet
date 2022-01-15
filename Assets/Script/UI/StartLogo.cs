using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLogo : MonoBehaviour
{

    public void Show() 
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowLogo());
    }

    IEnumerator ShowLogo() 
    {
        gameObject.GetComponent<Animation>().Play("Start");
        yield return new WaitForSeconds(1.15f);
        gameObject.GetComponent<Animation>().Play("StartOff");
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
