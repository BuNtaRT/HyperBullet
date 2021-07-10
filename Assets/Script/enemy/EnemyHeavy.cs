using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavy : EnemyAIBase
{
    protected override Color SetColor()
    {
        return ColorEnemy.Green_white;
    }
    protected override sbyte SetHp()
    {
        return 4;
    }
    protected override float SetSpeed()
    {
        return 1f + PlayerPrefs.GetFloat(PlayerPKey.SPEED_UP);
    }
}
