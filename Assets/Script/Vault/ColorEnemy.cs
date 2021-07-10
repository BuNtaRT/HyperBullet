using UnityEngine;

public static class ColorEnemy 
{
    public static readonly Color Red = new Color(1, 0, 0.390801f, 0.4f);
    public static readonly Color Auaq = new Color(0, 1, 0.9764706f, 0.4f);
    public static readonly Color Yellow = new Color(1, 0.69f, 0, 0.4f);
    public static readonly Color Purpule = new Color(0.58f, 0f, 1, 0.4f);
    public static readonly Color Green_white = new Color(0.5061855f, 0.8584906f, 0.5474501f, 0.4f);

    public static Color GetRandomColor() 
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                return Red;
            case 1:
                return Auaq;
            case 2:
                return Yellow;
            case 3:
                return Purpule;
            case 4:
                return Green_white;
            default:
                return Purpule;
        }

        
    }
}