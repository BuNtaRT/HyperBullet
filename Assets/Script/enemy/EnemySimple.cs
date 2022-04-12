using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : EnemyAIBase
{
    protected override float SetSpeed()
    {
        return 1.3f + PlayerPrefs.GetFloat(PlayerPKey.SPEED_UP);
    }

    protected override Color SetColor()
    {
        return ColorEnemy.Auaq;
    }
}
