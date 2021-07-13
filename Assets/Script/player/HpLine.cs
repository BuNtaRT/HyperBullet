using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HpLine 
{
    static int            _iCallRoad = 0;           //every 5 call hp be drawed
    static Color          _roadColor = new Color();
    static SpriteRenderer _maskRoad;

    public static void init(SpriteRenderer maskRoad) 
    {
        _maskRoad  = maskRoad;
        _roadColor = maskRoad.color;
    }

    public static void DrawCallRoadHp(float curretHP, float AllHp)
    {
        _iCallRoad++;
        if (_iCallRoad >= 5) // отрисовка раз в 5 вызовов 
        {
            _iCallRoad = 0;
            _roadColor = new Color(_roadColor.r, _roadColor.g, _roadColor.b, curretHP / AllHp);
            _maskRoad.color = _roadColor;
        }
    }

    public static Color ChangeColor(Color newColor) 
    {
        Color temp  = _roadColor;
        _roadColor = new Color(newColor.r, newColor.g, newColor.b, _roadColor.a); ;
        _maskRoad.color = _roadColor;
        return temp;
    }
}
