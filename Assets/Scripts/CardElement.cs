using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardElement : MonoBehaviour
{
    public CardInfo Info;
    [SerializeField] private Sprite _frontBaseCard;
    [SerializeField] private Sprite _backCard;
    [SerializeField] private Image _backPatern;
    [SerializeField] private Image _backGround;
    [SerializeField] private TMP_Text[] _cardNumberText;

    public void SetCard(CardInfo cardInfo, Sprite backPatern, Color colorBackground, Color colorBackPaterns)
    {
        _backGround.color = colorBackground;
        _backPatern.sprite = backPatern;
        _backPatern.color = colorBackPaterns;
        foreach (TMP_Text text in _cardNumberText)
        {
            text.text = cardInfo.Number;
        }
    }
}
