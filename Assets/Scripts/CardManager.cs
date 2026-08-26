using System;
using System.Collections.Generic;
using UnityEngine;

public enum CardCategory
{
    Hearts,
    Diamonds,
    Clubs,
    Spades,
}

public class CardManager : MonoBehaviour
{
    static public CardManager Instance;
    public Collider2D[] CardContainers;
    [SerializeField] public string[] CardNumbers;
    [SerializeField] private Sprite[] _backPaterns;
    [SerializeField] private CardElement _cardPrefab;
    [SerializeField] private Collider2D[] _storageContainers;
    [SerializeField] private PileContainer _pileContainer;
    [SerializeField] private Collider2D[] _columnContainers;
    private List<GameObject> _deck = new List<GameObject>();
    private Colors _colors;

    public void Awake()
    {
        Instance = this;

        CardContainers = new Collider2D[_storageContainers.Length + _columnContainers.Length];
        for (int i = 0; i < CardContainers.Length; i++)
        {
            if (i < _storageContainers.Length)
            {
                CardContainers[i] = _storageContainers[i];
            }
            else
            {
                CardContainers[i] = _columnContainers[i - _storageContainers.Length];
            }
        }

        _colors = gameObject.GetComponent<Colors>(); 
        DeackCreation();
        Deal();
    }

    private void DeackCreation()
    {
        foreach (CardCategory config in Enum.GetValues(typeof(CardCategory)))
        {
            for (int i = 0; i < CardNumbers.Length; i++)
            {
                InstantiateCardElement(
                    config,
                    CardNumbers[i],
                    _colors.GetColorConfig(config).Color1,
                    _colors.GetColorConfig(config).Color2
                    );
            }
        }
    }

    private void InstantiateCardElement(CardCategory config, string number, Color color1, Color color2)
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
            for (int j = 0; j <= i; j++)
            {
                GameObject card = _deck[UnityEngine.Random.Range(0, _deck.Count)];
                _columnContainers[i].gameObject.GetComponentInParent<ContainerElement>().AddElement(card.GetComponent<CardElement>());
                _deck.Remove(card);
            }
        }
        for (int i = 0; i < 24; i++)
        {
            GameObject randomCard = _deck[UnityEngine.Random.Range(0, _deck.Count)];
            _pileContainer.AddElement(randomCard.GetComponent<CardElement>());
            _deck.Remove(randomCard);
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
    public CardCategory Category;
    public string Number;
    [field: SerializeField] public bool IsReturn { get; private set; }
    public event Action<bool> CardIsReturnChanged;

    public CardInfo(CardCategory category, string number, bool isReturn = true)
    {
        Category = category;
        Number = number;
        IsReturn = isReturn;
    }

    public void SetIsReturn(bool isReturn)
    {
        if (IsReturn != isReturn)
        {
            CardIsReturnChanged.Invoke(isReturn);
            IsReturn = isReturn;
        }
    }
}
