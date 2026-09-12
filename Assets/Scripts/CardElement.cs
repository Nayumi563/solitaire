using TMPro;
using UnityEngine;

public class CardElement : MonoBehaviour
{
    public CardInfo Info;
    [SerializeField] private GameObject _backCard;
    [SerializeField] private GameObject _frontCard;
    [SerializeField] private SpriteRenderer _backPatern;
    [SerializeField] private SpriteRenderer _backGround;
    [SerializeField] private TMP_Text[] _cardNumberText;

    public void SetCardAtStart(CardInfo cardInfo, Sprite backPatern, Color colorBackground, Color colorBackPaterns)
    {
        Info = cardInfo;
        Info.CardIsReturnChanged += Return;
        _backGround.color = colorBackground;
        _backPatern.sprite = backPatern;
        _backPatern.color = colorBackPaterns;
        foreach (TMP_Text text in _cardNumberText)
        {
            text.text = CardManager.Instance.CardNumbers[cardInfo.Number - 1];
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
            gameObject.layer = 6;
        }
    }

    public bool CardIsRed()
    {
        if (Info.Category == CardCategory.Hearts || Info.Category == CardCategory.Diamonds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDisable()
    {
        Info.CardIsReturnChanged -= Return;
    }
}
