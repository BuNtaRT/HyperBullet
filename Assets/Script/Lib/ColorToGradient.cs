using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorToGradient
{
    public static Gradient Convert(Color color) 
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[1];
        colorKey[0].color = new Color(color.r, color.g, color.b);
        colorKey[0].time = 0f;
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1;
        alphaKey[0].time = 0;
        gradient.SetKeys(colorKey, alphaKey);

        return gradient;
    }
}
