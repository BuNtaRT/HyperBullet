using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtentions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="color">Entry color</param>
    /// <returns>Gradient(entry color alpha=1, entry color alpha=0)</returns>
    public static Gradient ConvertToGradient(this Color color)
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
