using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 _startDragPosition;
    private const float SCALE = 1.1f;
    private bool _isOnDrag;
    private bool _isOnColllider;
    private InputSystem_Actions InputActions;
    private InputAction _trackingAction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = Vector2.one * SCALE;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = Vector2.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startDragPosition = gameObject.transform.position;
        _isOnDrag = true;
    }

    private void Awake()
    {
        InputActions = new InputSystem_Actions();
        _trackingAction = InputActions.DragAndDrop.Tracking;
    }

    private void Update()
    {
        if (_isOnDrag)
        {
            gameObject.transform.position = new Vector3( Camera.main.ScreenToWorldPoint(_trackingAction.ReadValue<Vector2>()).x, Camera.main.ScreenToWorldPoint(_trackingAction.ReadValue<Vector2>()).y, -10);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isOnColllider = false;

        foreach (Collider2D collider in CardManager.Instance.StorageContainers)
        {
            if (collider.OverlapPoint(new Vector2(transform.position.x, transform.position.y)) && !_isOnColllider)
            {
                _isOnColllider = true;
                collider.gameObject.GetComponent<ContainerElement>().AddElement(gameObject.GetComponent<CardElement>(), ResetPosition);
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
    }

    private void OnEnable()
    {
        InputActions.DragAndDrop.Enable();
    }

    private void OnDisable()
    {
        InputActions.DragAndDrop.Disable();
    }
}
