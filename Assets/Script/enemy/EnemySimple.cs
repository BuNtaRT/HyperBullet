using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : EnemyAIBase
{
    protected override float SetSpeed()
    {
        return 1;
    }
    protected override Color SetColor()
    {
        return new Color(0, 1, 0.9764706f, 0.4f);
    }
}
