using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpell : MonoBehaviour
{
    // Keep active spell array on session game 
    // Spawn bonus on road
    [SerializeField]
    private List<SpellSO>            DebugSpellLiost = new List<SpellSO>();
    private Dictionary<SpellSO,bool> _lvlSpell       = new Dictionary<SpellSO, bool>();

    public void SetSpellList(List<SpellSO> SSO)
    {
        if (Application.isEditor)
            SSO = DebugSpellLiost;

        foreach (var item in SSO)
        {
            _lvlSpell.Add(item, false);
        }
    }

    public void Spawn(Vector3 position)
    {
        if (_lvlSpell.Count > 0 ) 
        {
            if (_lvlSpell.Count <= 2)
                _lvlSpell.SetFalseValue();

            SpellSO SpellToInit = _lvlSpell.GetRandom();
            Transform spellT    = ObjPool.Instance.SpawnObj(TypeObj.Spell, position);
            spellT.GetComponent<SpellOnRoad>().Set(SpellToInit);
        }
    }
}
