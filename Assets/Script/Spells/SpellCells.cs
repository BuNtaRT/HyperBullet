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

    // controll spellCell (image, script, activation) !! queue important !!
             [SerializeField] 
             List<Cell>                _cells;
    readonly Dictionary<int, Degrees> _degress      = new Dictionary<int, Degrees>();
             int                      _lastSite     = -1;
             bool                     _hide         = false;
            
    void Awake()
    {
        _degress.Add(0, new Degrees() { MinValue = 0, MaxValue = 90 });
        _degress.Add(1, new Degrees() { MinValue = 91, MaxValue = 180 });
        _degress.Add(2, new Degrees() { MinValue = -180, MaxValue = -90 });
        _degress.Add(3, new Degrees() { MinValue = -89, MaxValue = -1 });
    }

    //тут узнается на какую ячейку мы смотрим
    public void LookTo(float deg)
    {
        if (!_hide)
        {
            // благодаря такой конструкции сначала будет сверяться наша последняя ячейка, и уже если 
            // не правильно то будем искать по всем (типо оптимизация) 
            Degrees temp;
            if (_degress.TryGetValue(_lastSite, out temp))
            {
                if (!temp.Check(deg))
                {
                    int newSite = ReSearche(deg);
                    ReadyCell(false, _lastSite);
                    ReadyCell(true, newSite);
                    _lastSite = newSite;
                }
            }
            else
            {
                int newSite = ReSearche(deg);
                if (_lastSite == -1)
                {
                    _lastSite = newSite;
                    ReadyCell(true, newSite);
                }
            }
        }
    }

    public void StartState() 
    {
        _lastSite = -1;
        foreach (var item in _cells)
        {
            item.StartState();
        }
    }

    public void StandByState(bool enable) 
    {
        if (enable)
        {
            _hide = true;
            if (_lastSite >= 0)
                _cells[_lastSite].ReadyState(false);
        }
        else 
        {
            _hide = false;
            _lastSite = -1;
        }
    }

    public void EndState(bool pick) 
    {
        if (_lastSite >= 0 && pick)
        {
            ReadyCell(false, _lastSite);
            _cells[_lastSite].GetComponent<Cell>().Activate();
        }
        foreach (var item in _cells)
        {
            item.EndState();
        }
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

    void ReadyCell(bool ready,int index) 
    {
        if (index != -1)
        {
            _cells[index].ReadyState(ready);
        }
    }



}
