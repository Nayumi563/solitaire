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
    [SerializeField] private GameObject _button;//discard if not use
    private BoxCollider2D _collider;
    private Vector3 _startDragPosition;
    private Vector3 _mousePosition;

    private void Awake()
    {
        _collider = gameObject.GetComponent<BoxCollider2D>();
    }

    public void SetCardAtStart(CardInfo cardInfo, Sprite backPatern, Color colorBackground, Color colorBackPaterns)
    {
        Info = cardInfo;
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
        Info.IsReturn = isReturn;
        _backCard.SetActive(isReturn);
        _frontCard.SetActive(!isReturn);
        //_button.SetActive(!isReturn);//discard if not use
    }

    private void OnMouseDown()
    {
        _startDragPosition = gameObject.transform.position;
        _mousePosition = Input.mousePosition - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
    }

    public Vector3 GetMousePosition()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
        position.z = 0;
        return position;
    }
}
