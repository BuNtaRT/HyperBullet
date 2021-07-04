using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HpLine 
{
    static int iCallRoad = 0;
    static Color RoadColor = new Color();
    static SpriteRenderer maskRoad;

    public static void init(SpriteRenderer _maskRoad) 
    {
        maskRoad = _maskRoad;
        RoadColor = maskRoad.color;
    }

    public static void DrowCallRoadHp(float curretHP, float AllHp)
    {
        iCallRoad++;
        if (iCallRoad >= 5) // отрисовка раз в 5 вызовов 
        {
            iCallRoad = 0;
            RoadColor = new Color(RoadColor.r, RoadColor.g, RoadColor.b, curretHP / AllHp);
            maskRoad.color = RoadColor;
        }
    }
}
