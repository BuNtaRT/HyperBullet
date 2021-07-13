using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra_Sphere : MonoBehaviour,IPerk
{
    Color _pastColor;
    public void InitPerk()
    {
        SphereController.Instance.SetXFactor(0.5f);
        _pastColor = SphereController.Instance.SetColor(new Color(0.9320817f,0,1,0.7f));
    }

    public void DestroyPerk()
    {
        SphereController.Instance.SetXFactor(-0.5f);
        SphereController.Instance.SetColor(_pastColor);
        Destroy(this);
    }
}
