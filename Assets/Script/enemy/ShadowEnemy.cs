using System.Collections;
using UnityEngine;

public class ShadowEnemy : EnemyAIBase
{
    private float _timer;

    protected override Color SetColor()
    {
        return ColorEnemy.Purpule;
    }

    protected override float SetSpeed()
    {
        return 0.7f + PlayerPrefs.GetFloat(PlayerPKey.SPEED_UP);
    }

    protected override void LateInit()
    {
        _timer = Random.Range(1.5f,2f);
        Invoke(nameof(EnableShadowMode), _timer);
    }

    private void EnableShadowMode() 
    {
        if (!InSphere)
        {
            StartCoroutine(Shadow());
        }
    }

    IEnumerator Shadow() 
    {
        _enemyObj.ChangeLayer((int)ObjLayer.ShadowRoobots);
        yield return new WaitForSeconds(_timer);
        _enemyObj.ChangeLayer((int)ObjLayer.PropLight);
        yield return new WaitForSeconds(_timer);
        EnableShadowMode();
    }
}
