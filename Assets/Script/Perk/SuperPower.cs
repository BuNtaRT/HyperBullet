using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPower : MonoBehaviour, IPerk
{
    private Color _pastColor;
    public void InitPerk()
    {
        SphereController.Instance.SetPowerHealing(0.4f);
        _pastColor = HpLine.ChangeColor(new Color(0.6817942f,0,1));
    }

    public void DestroyPerk()
    {
        SphereController.Instance.SetPowerHealing(-0.4f);
        HpLine.ChangeColor(_pastColor);
        Destroy(gameObject);
    }
}
