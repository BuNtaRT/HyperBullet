using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDistribute : MonoBehaviour
{
    // Set spell and data on UI cells
    public static SpellDistribute Instance;

    [SerializeField]
    private List<Cell>  _cells  = new List<Cell>();


    private void Awake()
    {
        GlobalEventsManager.OnGetSpell.AddListener(SetSpell);
        if (Instance != null)
            throw new System.Exception("SpellDistribute over one on scene");
        else
            Instance = this;
    }

    public void SetSpell(SpellSO spell) 
    {
        //spellT.gameObject.AddComponent(Type.GetType(Enum.GetName(typeof(SpellScName), spell.NameSc)));
        bool complite = false;
        foreach (var item in _cells)
        {
            if (!item.GetStatus()) 
            {
                complite = true;
                item.SetSpell(spell);
                break;
            }
        }
        if (!complite) 
        {
            _cells[0].SetSpell(spell);
            Cell tempCell = _cells[0];
            _cells.Remove(tempCell);
            _cells.Add(tempCell);
        }
    }
}
