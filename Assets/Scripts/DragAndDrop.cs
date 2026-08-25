using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 _startDragPosition;
    private const float SCALE = 1.1f;
    private int _startOrderingLayer;
    private bool _isOnDrag;
    private bool _isOnColllider;
    private InputSystem_Actions InputActions;
    private InputAction _trackingAction;
    private CardElement _cardElement;

    private void Awake()
    {
        InputActions = new InputSystem_Actions();
        _trackingAction = InputActions.DragAndDrop.Tracking;
        _cardElement = gameObject.GetComponent<CardElement>();
    }

    private void Update()
    {
        if (_isOnDrag)
        {
            transform.position = new Vector3( Camera.main.ScreenToWorldPoint(_trackingAction.ReadValue<Vector2>()).x, Camera.main.ScreenToWorldPoint(_trackingAction.ReadValue<Vector2>()).y, -10);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector2.one * SCALE;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector2.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startDragPosition = transform.position;
        _startOrderingLayer = _cardElement.Canvas.sortingOrder;
        _cardElement.Canvas.sortingOrder = 20;
        _isOnDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isOnColllider = false;

        foreach (Collider2D collider in CardManager.Instance.CardContainers)
        {
            if (collider.OverlapPoint(new Vector2(transform.position.x, transform.position.y)) && !_isOnColllider)
            {
                _isOnColllider = true;
                collider.gameObject.GetComponentInParent<ContainerElement>().AddElement(_cardElement, ResetPosition);
                Debug.Log(collider);
            }
        }
        if (!_isOnColllider)
        {
            ResetPosition();
        }
        _isOnDrag = false;
    }

    private void ResetPosition()
    {
        transform.position = _startDragPosition;
        _cardElement.Canvas.sortingOrder = _startOrderingLayer;
    }

    private void OnEnable()
    {
        _trackingAction.Enable();
    }

    private void OnDisable()
    {
        _trackingAction.Disable();
    }
}
