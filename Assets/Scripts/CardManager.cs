using System;
using UnityEngine;

public enum CardConfig
{
    Hearts,
    Diamonds,
    Clubs,
    Spades,
}

public class CardManager : MonoBehaviour
{
    private Colors _colors;
    [SerializeField] private string[] _cardNumbers;
    [SerializeField] private Sprite[] _backPaterns;
    [SerializeField] private CardElement _cardPrefab;
    [SerializeField] private Transform _cardContainer;

    public void Awake()
    {
        _colors = gameObject.GetComponent<Colors>();
    }
    public void DeackCreation()
    {
        foreach (CardConfig config in Enum.GetValues(typeof(CardConfig)))
        {
            for (int i = 0; i < _cardNumbers.Length; i++)
            {
                InstantiateCardElement(
                    config,
                    _cardNumbers[i],
                    new Vector3(i, 0, -i * 0.1f),
                    _colors.GetColorConfig(config).Color1, 
                    _colors.GetColorConfig(config).Color2
                    );
            }
        }
    }
    private void InstantiateCardElement(CardConfig config, string number, Vector3 position, Color color1, Color color2)
    {
        MainGame mainGame = gameObject.GetComponent<MainGame>();
        CardElement card = Instantiate(_cardPrefab.gameObject, _cardContainer.position + position, Quaternion.identity, _cardContainer).GetComponent<CardElement>();
        card.SetCard(
            new CardInfo(config, number), 
            RandomizeTexture(_backPaterns),
            color1,
            color2
            );
    }
    private Sprite RandomizeTexture(Sprite[] sprites)
    {
        int index = UnityEngine.Random.Range(0, sprites.Length);
        return sprites[index];
    }
}
[System.Serializable]
public class CardInfo
{
    public CardConfig Config;
    public string Number;
    public bool IsReturn;
    public CardInfo(CardConfig config, string number, bool isReturn = false)
    {
        Config = config;
        Number = number;
        IsReturn = isReturn;
    }
}
