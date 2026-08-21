using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardElement : MonoBehaviour
{
    public CardInfo Info;
    [SerializeField] private GameObject _backCard;
    [SerializeField] private GameObject _frontCard;
    [SerializeField] private Image _backPatern;
    [SerializeField] private Image _backGround;
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
            text.text = cardInfo.Number;
        }
        Return(cardInfo.IsReturn);
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

    private void OnDisable()
    {
        Info.CardIsReturnChanged -= Return;
    }
}
