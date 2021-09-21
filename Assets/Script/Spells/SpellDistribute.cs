using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDistribute : MonoBehaviour
{
    [SerializeField]
    List<Cell> _cells = new List<Cell>();
    public void SetSpell() 
    {
        foreach (var item in _cells)
        {
            if(!item.Activ())
            {
                item.SetSpell();
            }
        }
    }
}
