using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    protected SpellSO _spell;
    protected Vector3 _touchPoint;
    public virtual void Init(SpellSO spell) 
    {
        _spell = spell;
        if (_spell.Area != 0)
        {
            TapControll.Instance.GetNextPoint(SetPoint);
        }
        else 
        {
            InitOverride();
        }
    }

    public void SetPoint(Vector3 point) 
    {
        _touchPoint = point;
        InitOverride();
    }

    protected virtual void InitOverride() 
    {
        
    }

}
