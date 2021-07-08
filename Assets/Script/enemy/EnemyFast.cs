using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFast : EnemyAIBase
{
    protected override float SetSpeed()
    {
        return 2;
    }
    protected override Color SetColor()
    {
        return new Color(1, 0, 0.390801f, 0.4f);
    }
}
