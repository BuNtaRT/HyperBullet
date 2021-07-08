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
    Extra_Sphere,
    TwoX_PowerBullet,
}

public enum PerkRarity : byte
{
    Standart,
    Rare,
    Epic,
    Legendary
}