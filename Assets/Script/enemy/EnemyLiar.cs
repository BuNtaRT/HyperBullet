using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLiar : EnemyAIBase
{
    protected override Color SetColor()
    {
        return ColorEnemy.GetRandomColor();
    }
    protected override sbyte SetHp()
    {
        return (sbyte)Random.Range(1,3);
    }
    protected override float SetSpeed()
    {
        return Random.Range(0.7f,2f) + PlayerPrefs.GetFloat(PlayerPKey.SPEED_UP);
    }
}
