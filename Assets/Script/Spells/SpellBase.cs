using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    protected SpellSO _spell;
    protected Vector3 _touchPoint;
    protected Transform _mapContainer;
    public virtual void Init(SpellSO spell) 
    {
        _spell = spell;
        LvlSpellInit();

        if (_spell.Area)
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

    protected void LookAtProp(Transform prop) 
    {
        prop.position = new Vector3(0, 0.5f, 0);
        prop.LookAt(_touchPoint);
        prop.eulerAngles = new Vector3(0, prop.eulerAngles.y, 0);
    }

    protected virtual void InitOverride() {}
    protected virtual void LvlSpellInit() {}
    protected virtual void EndAnimation() 
    {
        _mapContainer = CutScene.Instance.GetMapContainer();
    }


}
