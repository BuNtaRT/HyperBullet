using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceBonus : MonoBehaviour
{
    #region Chances
    // шанс на выпадение бонуса
    public int ChanceForBonus = 100;
    // для перков
    public int ChanceForPerk  = 100;
    // для карточных бонусов
    public int ChanceForSpell = 100;
    #endregion
    [SerializeField] SpawnPerks       _perkCall;
    [SerializeField] SpawnSpell       _spellCall;


    private void Awake()
    {
        GlobalEventsManager.OnEnemyKill.AddListener(EnemyDie);
    }


    public void EnemyDie(Transform transformEnemy)
    {
        if (Random.Range(0, 100) <= ChanceForBonus)
        {
            //if (ChanceForPerk >= Random.Range(0, 100))
            //   _perkCall.Spawn(transformEnemy.position);
            if (ChanceForSpell >= Random.Range(0, 100))
                _spellCall.Spawn(transformEnemy.position);

        }
    }
}
