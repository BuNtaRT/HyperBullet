using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HpLine 
{
    static int            _iCallRoad = 0;           //every 5 call hp be drawed
    static Color          _RoadColor = new Color();
    static SpriteRenderer _maskRoad;

    public static void init(SpriteRenderer maskRoad) 
    {
        _maskRoad  = maskRoad;
        _RoadColor = maskRoad.color;
    }

    public static void DrawCallRoadHp(float curretHP, float AllHp)
    {
        _iCallRoad++;
        if (_iCallRoad >= 5) // отрисовка раз в 5 вызовов 
        {
            _iCallRoad = 0;
            _RoadColor = new Color(_RoadColor.r, _RoadColor.g, _RoadColor.b, curretHP / AllHp);
            _maskRoad.color = _RoadColor;
        }
    }
}
