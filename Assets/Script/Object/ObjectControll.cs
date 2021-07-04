using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControll : MonoBehaviour
{
    public static ObjectControll singleton { private set; get; }

    Animation BlackScreen;
    private void Start()
    {
        BlackScreen = GameObject.Find("InGameUI/Black").GetComponent<Animation>();
    }

    public void BlackScOn() { BlackScreen.Play("BlackScreen"); }
    public void BlackScOff() { BlackScreen.Play("BlackScreenOff"); }
}
