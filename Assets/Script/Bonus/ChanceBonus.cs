using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceBonus : MonoBehaviour
{
    #region Chances
    // шанс на выпадение бонуса
    public int ChanceForBonus = 100;
    // для перков
    public int ChanceForPerk = 100;
    // для карточных бонусов
    public int ChanceForSpell = 100;
    #endregion
    public static    ChanceBonus Instance { get; private set; }
    [SerializeField] SpawnPerks       _perkCall;
    [SerializeField] SpawnSpell       _spellCall;


    private void Awake()
    {
        Instance = this;
    }


    public void EnemyDie(Vector3 position)
    {
        if (Random.Range(0, 100) <= ChanceForBonus)
        {
            if (ChanceForSpell >= Random.Range(0, 100))
                _spellCall.Spawn(position);
            //if (ChanceForPerk >= Random.Range(0, 100))
             //   _perkCall.Spawn(position);
        }
    }
}
