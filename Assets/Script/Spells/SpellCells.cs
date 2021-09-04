using System.Collections.Generic;
using UnityEngine;

public class SpellCells : MonoBehaviour
{
    struct Cell
    {
        public string         Name;
        public SpriteRenderer Ico;
        public Color Color;
    }

    Dictionary<int, Cell> _cells = new Dictionary<int, Cell>();
    [SerializeField] List<SpriteRenderer> _cellsSprite;
    [SerializeField] List<Color> _cellsColor;
    int _countActive = 0;

    public void SetNew(string nameSpell) 
    {
        if (_countActive >= 3) 
            _countActive = 0;

        //_cells.Add();

        //_cellsSprite[_countActive].sprite = 
        
    }
    public void SetSaturation(float saturation) 
    {
        Mathf.Clamp(saturation, 0.4f, 1);
        foreach (SpriteRenderer temp in _cellsSprite)
            temp.color = new Color(temp.color.r, temp.color.g, temp.color.b,saturation);
    }
}
