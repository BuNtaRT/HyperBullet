using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Тут хранятся все обьекты которые не нужно отрисовывать при обычной игре но, будет необходимо показать при вставке
// тут они так же убираются и снова активируются

public class CastSceneSwitcher : MonoBehaviour
{
    public List<GameObject> HideOnGame;
    public List<Light> LightHideOnGame;
    public List<GameObject> ShowOnGame;

    public void NowGame_HideAll() 
    {
        Debug.Log("NOw GameHideAll");
        Action(false);
    }
    public void NowCast_ShowAll() 
    {
        Action(true);
    }
    public void Action(bool ac) 
    {
        foreach (GameObject temp in HideOnGame)
        {
            temp.SetActive(ac);
        }
        foreach (Light temp in LightHideOnGame)
        {
            temp.enabled=ac;
        }
        foreach (GameObject temp in ShowOnGame) 
        {
            temp.SetActive(!ac);
        }
    }
}
