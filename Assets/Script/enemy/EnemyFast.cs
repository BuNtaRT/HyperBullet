using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFast : EnemyAIBase
{
    protected override float SetSpeed()
    {
        return 2.5f + PlayerPrefs.GetFloat(PlayerPKey.SPEED_UP);
    }
    protected override Color SetColor()
    {
        return ColorEnemy.Red;
    }


}
