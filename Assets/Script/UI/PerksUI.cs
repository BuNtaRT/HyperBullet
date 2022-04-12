using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerksUI : MonoBehaviour
{
    // тут UI взаимодействие игрока с перками 
    public PerkSO      PerkNow;

    #region UI
    public GameObject  WindowPerk;

    [Header("Text")]
    public Text        NamePerk;
    public Text        DescrPerk;

    [Header("Particle")]
    public GameObject  Particle1;
    public GameObject  Particle2;

    [Header("Animation")]
    public Animator    Card;
    public Animator    Buttons;
    public Animator    TextObj;

    [Header("Card")]
    public Image       IcoPerk;
    public Transform   Borders;
    #endregion

    private void Awake()
    {
        GlobalEventsManager.OnGetPerk.AddListener(PerkShow);
    }

    public void ChoisePerk(bool choise) 
    {
        if (choise)
        {
            Card.Play("PickPerk");
            PerkNI.Instance.ShowAndInitPerk(PerkNow);
        }
        else 
        {
            Card.Play("HideCard");
        }
        Buttons.Play("ButtonsHide");
        TextObj.Play("TextHide");
        StartCoroutine(ClosePerks());
    }

    private IEnumerator ClosePerks()
    {
        yield return new WaitForSecondsRealtime(0.9f);
        GlobalEventsManager.InvokPause(PauseStatus.pickPerk,false);
        WindowPerk.SetActive(false);
    }

    // когда мы взяли перк с дороги то выводим о нем информацию 
    public void PerkShow()
    {
        GlobalEventsManager.InvokPause(PauseStatus.pickPerk, true);

        string name;
        string desc;

        if (PlayerPrefs.GetInt(PlayerPKey.LANGUAGE) == (int)PlayerPKey.langKey.ru)
        {
            name = PerkNow.NameRU;
            desc = PerkNow.DescriptionRU;
        }
        else 
        {
            name = PerkNow.NameEU;
            desc = PerkNow.DescriptionEU;
        }

        NamePerk.text = name;
        DescrPerk.text = desc;
        IcoPerk.sprite = PerkNow.Ico;

        TextObj.Play("TextShow");
        Buttons.Play("ButtonShow");
        Card.Play("cardShow");
        WindowPerk.SetActive(true);
        ColorPerk();
        WindowPerk.SetActive(true);
    }

    // цвет карточки устанавливается тут 
    private void ColorPerk() 
    {
        float H, S, V;
        Color.RGBToHSV(PerkNow.MainColor,out H,out S,out V);
        Color TextColor = Color.HSVToRGB(H, S+0.15f, V-0.2f);
        Color Border = Color.HSVToRGB(H, S - 0.2f, V-0.5f);
        Card.GetComponent<Image>().color = PerkNow.MainColor;
        NamePerk.color = TextColor;
        DescrPerk.color = TextColor;
        foreach (Transform temp  in Borders) 
        {
            temp.GetComponent<Image>().color = Border;
        }

        var col = Particle1.GetComponent<ParticleSystem>().colorOverLifetime;
        col.color = PerkNow.GradientParticle;
        col = Particle2.GetComponent<ParticleSystem>().colorOverLifetime;
        col.color = PerkNow.GradientParticle;
    }
}
