using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "New Spell", menuName = "Bonus/Spell")]
public class SpellSO : ScriptableObject
{
    public string        NameRU;
    public string        NameEU;
    public float         Price;
    public SpellScName   NameSc;
    [Range(0,360)]
    public int           Area;
    public Sprite        Ico;
    public PlayableAsset Animation;
    
}


public enum SpellScName
{
    SwordTrow,
}