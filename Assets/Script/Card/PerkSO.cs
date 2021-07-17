using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "New Perk", menuName = "Bonus/Perk")]
public class PerkSO : ScriptableObject
{
    public PerkRarity rarity;
    public string NameRU;
    public string NameEU;

    public string DescriptionRU;
    public string DescriptionEU;

    public Sprite Ico;
    public PerkName perkName;

    public Gradient GradientParticle;
    public Color MainColor;
}

public enum PerkName : byte
{
    // названия классов, точ в точ
    Prediction,
    Extra_Sphere,
    Fast_Bullet,
    Slow_Mo_Bullet,
    TwoX_PowerBullet,
    Bursting_Bullet,
    Ricochet_Bullet,
    Slow_Mo_shoot,
    SuperPower,
    SuperDiscontr,
    ExplBullet,
    Clone,
    Healt,
    SuperPowerShealt,
    EmiBullet,
    TwoX_Damage,
}

public enum PerkRarity : byte
{
    Standart,
    Rare,
    Epic,
    Legendary
}