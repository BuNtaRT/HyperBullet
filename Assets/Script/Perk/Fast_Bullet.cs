using UnityEngine;

public class Fast_Bullet : MonoBehaviour, IPerk
{
    public void InitPerk()
    {
        BulletBase.SetColor(ColorToGradient.Convert(new Color(1, 0.8066778f, 0f)));
        BulletBase.SetSpeed(0.5f);
    }
    public void DestroyPerk()
    {
        BulletBase.SetDefault();
        Destroy(gameObject);
    }
}
