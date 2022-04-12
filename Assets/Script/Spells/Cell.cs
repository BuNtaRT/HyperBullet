using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private class Saturation
    {
        public const float DisableSP   = 0.3f;
        public const float EnableSP    = 0.7f;
        public const float ReadySP     = 0.9f;

        public const float DisableText = 0f;
        public const float EnableText  = 0.2f;
        public const float ReadyText   = 1f;
    }

    [SerializeField]
    private Text           _priceText;
    private SpriteRenderer _sprite;
    private float          _price;
    private SpellSO        _spell;


    private const float BIG_SCALE_CELL    = 1.05f;
    private const float NORMAL_SCALE_CELL = 0.6f;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        if (_sprite.enabled && SessionBullet.Instance.WriteOff(_price) )
        {
            _sprite.enabled = false;
            SetSaturation(Saturation.DisableSP, Saturation.DisableText);

            GameObject spellTempObj = new GameObject();
            Type typeSpell = Type.GetType(Enum.GetName(typeof(SpellScName), _spell.NameSc));
            spellTempObj.gameObject.AddComponent(typeSpell);
            spellTempObj.GetComponent<SpellBase>().Init(_spell);

        }
    }

    public bool GetStatus() => _sprite.enabled;

    public void SetSpell(SpellSO spell)
    {
        float curretScale = _sprite.transform.localScale.z;
        _spell = spell;
        if (!GetStatus())
        {
            _sprite.enabled = true;
            _sprite.sprite = null;
            SetSaturation(Saturation.EnableSP, 0);
        }
        _sprite.transform.DOScale(curretScale*2,0.5f).OnComplete(()=> 
        {
            _sprite.sprite = spell.Ico;
            _price = spell.Price;
            _priceText.text = spell.Price.ToString();
            SetSaturation(Saturation.EnableSP, Saturation.EnableSP);
            _sprite.transform.DOScale(curretScale, 0.5f);
        });
    }

    public void ReadyState(bool ready)
    {
        if (_sprite.enabled)
        {
            float scale;
            if (ready)
            {
                scale = BIG_SCALE_CELL;
                SetSaturation(Saturation.ReadySP,Saturation.ReadyText);
            }
            else
            {
                scale = NORMAL_SCALE_CELL;
                SetSaturation(Saturation.EnableSP,Saturation.EnableText);
            }

            transform.DOScale(scale, 0.25f).SetUpdate(true);
        }
    }

    public void StartState()
    {
        if (_sprite.enabled)
        {
            SetSaturation(Saturation.EnableSP,Saturation.EnableText);
        }
    }

    public void EndState() 
    {
        if (_sprite.enabled)
        {
            SetSaturation(Saturation.DisableSP,Saturation.DisableText);
        }
    }

    void SetSaturation(float satSP,float satPrice)
    {
        _sprite   .DOColor(new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, satSP), 0.25f).SetUpdate(true);
        _priceText.DOColor(new Color(_priceText.color.r, _priceText.color.g, _priceText.color.b, satPrice), 0.25f).SetUpdate(true);
    }
}
