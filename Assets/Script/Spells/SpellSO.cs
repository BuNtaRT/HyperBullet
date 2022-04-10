using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "New Spell", menuName = "Bonus/Spell")]
public class SpellSO : ScriptableObject
{
    public string        NameRU;
    public string        NameEU;
    public float         DefaultPrice;
    public float         Price;
    public float         MarketPrice;
    public SpellScName   NameSc;
    public bool          Area;
    public Sprite        Ico;
    public PlayableAsset Animation;
    [Range(1,3)]
    public int           Lvl;

    public string DescriptionLvl_RU;
    public string DescriptionLvl2_RU;
    public string DescriptionLvl3_RU;
    
}


public enum SpellScName
{
    SwordTrow,
    SwordHit,
}