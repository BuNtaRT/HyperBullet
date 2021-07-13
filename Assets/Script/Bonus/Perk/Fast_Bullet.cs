using UnityEngine;

public class Fast_Bullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletSource.SetColor(ColorToGradient.Convert(new Color(1, 0.8066778f, 0f)));
        BulletSource.SetSpeed(0.5f);
    }
    public void DestroyPerk()
    {
        BulletSource.SetDefault();
        Destroy(gameObject);
    }
}
