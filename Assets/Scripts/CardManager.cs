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
    static public CardManager Instance;
    private Colors _colors;
    [SerializeField] private string[] _cardNumbers;
    [SerializeField] private Sprite[] _backPaterns;
    [SerializeField] private CardElement _cardPrefab;
    [SerializeField] public Collider2D[] StorageContainer;
    [SerializeField] private Transform _pileContainer;
    [SerializeField] private Transform[] _columnContainers;
    private List<GameObject> _deck = new List<GameObject>();

    public void Awake()
    {
        Instance = this;
        _colors = gameObject.GetComponent<Colors>();
    }

    private void Start()
    {
        DeackCreation();
        Deal();
    }
    private void DeackCreation()
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
        card.GetComponent<CardElement>().SetCardAtStart(
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
            for (int j = 0; j < i; j++)
            {
                GameObject card = _deck[UnityEngine.Random.Range(0, _deck.Count)];
                _deck.Remove(card);
                card.transform.SetParent(_columnContainers[i]);
                card.transform.localScale = new Vector3(1,1,1);
                card.transform.position = new Vector3(0, - j * 0.3f, -j * 0.1f) + _columnContainers[i].position;
                if (i - 1 == j)
                    card.GetComponent<CardElement>().Return(false);
            }
        }
        foreach (GameObject card in _deck)
        {
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
    public CardInfo(CardConfig config, string number, bool isReturn = true)
    {
        Config = config;
        Number = number;
        IsReturn = isReturn;
    }
}
