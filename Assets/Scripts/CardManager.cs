using System;
using System.Collections.Generic;
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
    [SerializeField] private Transform _pileContainer;
    [SerializeField] private Transform[] _columnContainers;
    private List<GameObject> _deck = new List<GameObject>();

    public void Awake()
    {
        _colors = gameObject.GetComponent<Colors>();
    }

    private void Start()
    {
        Deal();
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
                    _colors.GetColorConfig(config).Color1, 
                    _colors.GetColorConfig(config).Color2
                    );
            }
        }
    }
    private void InstantiateCardElement(CardConfig config, string number, Color color1, Color color2)
    {
        GameObject card = Instantiate(_cardPrefab.gameObject);
        card.GetComponent<CardElement>().SetCard(
            new CardInfo(config, number), 
            RandomizeTexture(_backPaterns),
            color1,
            color2
            );
        _deck.Add(card);
    }
    private void Deal()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < i + 1; j++)
            {
                GameObject card = _deck[UnityEngine.Random.Range(0, _deck.Count)];
                _deck.Remove(card);
                card.transform.SetParent(_columnContainers[i]);
                card.transform.localScale = new Vector3(1,1,1);
                card.transform.position = new Vector3(0, - j * 0.5f, - j) + _columnContainers[i].position;
            }
        }
        foreach (GameObject card in _deck)
        {
            //_deck.Remove(card);
            card.transform.SetParent(_pileContainer);
            card.transform.localScale = new Vector3(1, 1, 1);
            card.transform.position = new Vector3(0, 0, UnityEngine.Random.Range(0, - _deck.Count)) + _pileContainer.position;
        }
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
