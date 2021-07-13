using UnityEngine;

public class Fast_Bullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        Gradient trailColor = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[1];
        colorKey[0].color = new Color(1, 0.8066778f, 0f);
        colorKey[0].time = 0f;
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1;
        alphaKey[0].time = 0;
        trailColor.SetKeys(colorKey, alphaKey);

        BulletSource.SetColor(trailColor);
        BulletSource.SetSpeed(0.5f);
    }
    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);

    }
}
