using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "New Perk", menuName = "Bonus/Perk")]
public class PerkSO : ScriptableObject
{
    public int rare=0;
    string[] optionRare = new string[] { "Стандартный", "Редкий", "Эпический", "Легендарный" };
    public string NameRU;
    public string NameEU;

    public string DescriptionRU;
    public string DescriptionEU;

    public Sprite Ico;
    public PerkName perkName;

    public Gradient GradientParticle;
    public Color MainColor;


}

public enum PerkName
{
    ExtraSphere,
    TwoX_PowerBullet,
} 