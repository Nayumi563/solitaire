using UnityEngine;

public class CardManager : MonoBehaviour
{
    private const int _cardsNbr = 52;
    private const int _cardsColorNbr = 4;
    [SerializeField] private string[] _cardNumbers;
    [SerializeField] private Sprite[] _backPaterns;
    [SerializeField] private CardElement _cardPrefab;
    [SerializeField] private Transform _cardContainer;
    
    public void DeackCreation()
    {
        int count = 0;
        foreach (string cardNumber in _cardNumbers)
        {
            InstantiateCardElement(cardNumber, new Vector3(count, 0, - count * 0.1f));
            count ++;
        }
    }
    private void InstantiateCardElement(string cardNumber, Vector3 position)
    {
        MainGame mainGame = gameObject.GetComponent<MainGame>();
        CardElement card = Instantiate(_cardPrefab.gameObject, _cardContainer.position + position, Quaternion.identity, _cardContainer).GetComponent<CardElement>();
        card.SetCard(
            new CardInfo(mainGame.ColorNameKeys[0], cardNumber), 
            RandomizeTexture(_backPaterns), 
            mainGame.ColorsDictionary[mainGame.ColorNameKeys[0]], 
            mainGame.ColorsDictionary[mainGame.ColorNameKeys[1]]
            );
    }

    private Sprite RandomizeTexture(Sprite[] sprites)
    {
        int index = UnityEngine.Random.Range(0, sprites.Length);
        return sprites[index];
        //RandomizeColor(spriteRenderer);
    }
    //private void RandomizeColor(SpriteRenderer spriteRenderer)
    //{
    //    string color = MainGame.ColorNames[UnityEngine.Random.Range(0, MainGame.ColorNames.Length)];
    //    spriteRenderer.color = MainGame.ColorsDictionary[color];
    //}
}
[System.Serializable]
public class CardInfo
{
    public string PrincipalColor;
    public string Number;
    public bool IsReturn;
    public CardInfo(string principalColor, string number, bool isReturn = false)
    {
        PrincipalColor = principalColor;
        Number = number;
        IsReturn = isReturn;
    }
}
