using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellOnRoad : ItemOnRoadBase, IItemOnRoad
{
    SpellSO _spell;

    private void Awake()
    {
        _objectT = TypeObj.Spell;
        InitAwake();
    }
    public void Set(SpellSO spell) 
    {
        _spell      = spell;
        _ico.sprite = spell.Ico;
        InitAnim();
    }
    public void Pick() 
    {
        GlobalEventsManager.SendSpell(_spell);
        End();
        ObjPool.Instance.Destroy(TypeObj.Spell, gameObject);
    }
}
