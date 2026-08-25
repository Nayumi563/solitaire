using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardElement : MonoBehaviour
{
    public CardInfo Info;
    public List<CardElement> Childs = null;
    [SerializeField] private GameObject _backCard;
    [SerializeField] private GameObject _frontCard;
    [SerializeField] private Image _backPatern;
    [SerializeField] private Image _backGround;
    [SerializeField] private TMP_Text[] _cardNumberText;
    [field: SerializeField] public Canvas Canvas { get; private set; }

    public void SetCardAtStart(CardInfo cardInfo, Sprite backPatern, Color colorBackground, Color colorBackPaterns)
    {
        Info = cardInfo;
        Info.CardIsReturnChanged += Return;
        _backGround.color = colorBackground;
        _backPatern.sprite = backPatern;
        _backPatern.color = colorBackPaterns;
        foreach (TMP_Text text in _cardNumberText)
        {
            text.text = cardInfo.Number;
        }
        Return(cardInfo.IsReturn);
        name = $"{cardInfo.Number} {cardInfo.Category} card";
    }

    public void Return(bool isReturn)
    {
        _backCard.SetActive(isReturn);
        _frontCard.SetActive(!isReturn);
        if (isReturn)
        {
            gameObject.layer = 2;
        }
        else
        {
            gameObject.layer = 0;
        }
    }

    public void AddChild(CardElement element)
    {
        if (Childs == null) Childs = new List<CardElement>();
        Childs.Add(element);
        if (element.Childs != null) {
            foreach (CardElement child in element.Childs) {
                Childs.Add(child);
            }
        }
    }

    private void OnDisable()
    {
        Info.CardIsReturnChanged -= Return;
    }
}
