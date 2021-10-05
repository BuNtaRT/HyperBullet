using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpell : MonoBehaviour
{
    [SerializeField]
    List<SpellSO> _lvlSpell = new List<SpellSO>();
    public void SetSpellList(List<SpellSO> SSO) => _lvlSpell = SSO;

    public void Spawn(Vector3 position)
    {
        Debug.Log("SpawnSpell");
        if (_lvlSpell.Count > 0 ) 
        {
            Transform spellT = ObjPool.Instance.SpawnObj(TypeObj.Spell, position);


            spellT.gameObject.AddComponent(Type.GetType(_lvlSpell[0].Name));

            spellT.GetComponent<SpellBase>().InitSpell();
        }
    }
}
