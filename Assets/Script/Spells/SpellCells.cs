using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class SpellCells : MonoBehaviour
{
    class Degrees
    {
        public int MaxValue;
        public int MinValue;
        public bool Check(float value) 
        {
            if (value >= MinValue && value <= MaxValue)
                return true;
            else
                return false;
        }
    }

    const float BIG_SCALE_CELL    = 3.5f;
    const float NORMAL_SCALE_CELL = 2f;

    // controll spellCell (image, script, activation) !! queue important !!
    [SerializeField] 
    List<SpriteRenderer>     _cellsSprite;
    Dictionary<int, Degrees> _degress      = new Dictionary<int, Degrees>();
    int                      _lastSite     = -1;
    

    private void Awake()
    {
        _degress.Add(0, new Degrees() { MinValue = 0, MaxValue = 90 });
        _degress.Add(1, new Degrees() { MinValue = 91, MaxValue = 180 });
        _degress.Add(2, new Degrees() { MinValue = -180, MaxValue = -90 });
        _degress.Add(3, new Degrees() { MinValue = -89, MaxValue = -1 });
    }

    public void LookTo(float deg) // out Site
    {
        // благодаря такой конструкции сначала будет сверяться наша последняя ячейка, и уже если 
        // не правильно то будем искать по всем (типо оптимизация) 
        Degrees temp;

        if (_degress.TryGetValue(_lastSite, out temp))
        {
            if (!temp.Check(deg))
            {
                int newSite = ReSearche(deg);
                ResizeCell(false,_lastSite);
                ResizeCell(true, newSite);
                _lastSite = newSite;
                Debug.Log(_lastSite);
            }
        }
        else
        {
            int newSite = ReSearche(deg);
            if (_lastSite == -1) 
            {
                _lastSite = newSite;
                ResizeCell(true, newSite);
                Debug.Log(_lastSite);

            }
        }
    }

    public void Pick(float deg) 
    {
        ResizeCell(false, _lastSite);
        int targetCell = ReSearche(deg);
        _cellsSprite[targetCell].GetComponent<Cell>().Activate();
    }

    public void DontPick() 
    {
        ResizeCell(false, _lastSite);
    }

    public void SetSaturation(float saturation) 
    {
        saturation = Mathf.Clamp(saturation, 0.3f, 1);
        foreach (SpriteRenderer temp in _cellsSprite)
            temp.DOColor(new Color(temp.color.r, temp.color.g, temp.color.b,saturation),0.25f).SetUpdate(true);
    }

    public void Reinit() 
    {
        _lastSite = -1;
    }

    int ReSearche(float deg) // осуществяем полный поиск по углу и возвращаем сторону
    {
        foreach (var item in _degress)
        {
            if (item.Value.Check(deg))
            {
                return item.Key;
            }
        }
        return 0;
    }

    void ResizeCell(bool up,int index) 
    {
        float scale;
        if (up)
            scale = BIG_SCALE_CELL;
        else
            scale = NORMAL_SCALE_CELL;

        _cellsSprite[index].transform.DOScale(scale,0.25f).SetUpdate(false);
    }


}
