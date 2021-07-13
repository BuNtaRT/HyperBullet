using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletSource
{   
    //in this sc contains customize data of bullet

    static BulletData _bulletData;

    public static void SetDefault() 
    {
        _bulletData.Hp = 1;
        _bulletData.Damage = 2;
        _bulletData.Speed = 1;

        Gradient temp = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[1];
        colorKey[0].color = new Color(0,1, 0.9831753f);
        colorKey[0].time = 0f;
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1;
        alphaKey[0].time = 0;
        temp.SetKeys(colorKey,alphaKey);

        _bulletData.TrailColor = temp;
        _bulletData.NameModify = "";
    }

    public static void SetHp    (int hp) =>   _bulletData.Hp     = hp > 0   ? (sbyte)hp : (sbyte)1;
   
    public static void SetDamage(int dm) =>   _bulletData.Damage = dm > 0   ? (sbyte)dm : (sbyte)2;
   
    public static void SetSpeed (float sp) => _bulletData.Speed  = sp > 0.1 ? sp : 2;
  
    public static void SetColor (Gradient col) => _bulletData.TrailColor = col;
    public static void SetModify (string nameMethod) => _bulletData.NameModify = nameMethod;

    public static BulletData GetConf() 
    {
        return _bulletData;
    }

}
public struct BulletData
{
    public sbyte Hp;
    public sbyte Damage;
    public float Speed;
    public Gradient TrailColor;
    public string NameModify;
}
