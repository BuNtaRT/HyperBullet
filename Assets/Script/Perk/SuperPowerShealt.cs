using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerShealt : MonoBehaviour, IPerk
{
    private Color _pastColorSphere;
    private Color _pastColorRoad;

    public void InitPerk()
    {
        _pastColorSphere = SphereController.Instance.SetColor(new Color(0.9320817f, 0, 1, 0.7f));
        SphereController.Instance.SetXFactor(0.5f);
        SphereController.Instance.SetPowerHealing(0.5f);
        _pastColorRoad = HpLine.ChangeColor(new Color(0.6817942f, 0, 1));
    }

    public void DestroyPerk()
    {
        SphereController.Instance.SetPowerHealing(-0.5f);
        HpLine.ChangeColor(_pastColorRoad);
        SphereController.Instance.SetXFactor(-0.5f);
        SphereController.Instance.SetColor(_pastColorSphere);
        Destroy(gameObject);
    }
}
