using UnityEngine;
using UnityEngine.InputSystem;

public class DargAndDrop : MonoBehaviour
{
    private bool _isDragging;
    private bool _isSelected;
    [SerializeField] private LayerMask _draggableMask;
    private InputSystem_Actions _inputActions;
    private InputAction _trackingAction;
    private InputAction _clickingAction;
    private RaycastHit2D _hit;
    private CardElement _selectedCard;
    private Vector2 _offsetMouse;
    private Vector3 _startDragPosition;
    private ContainerElement _startDragContainer;
    private const float SCALE = 1.1f;
    private bool _isOnColllider;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _trackingAction = _inputActions.DragAndDrop.Tracking;
        _clickingAction = _inputActions.DragAndDrop.Clicking;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(_trackingAction.ReadValue<Vector2>().x, _trackingAction.ReadValue<Vector2>().y, Camera.main.transform.position.z * 100f));
        _hit = Physics2D.Raycast(ray.origin, ray.direction, 200000, _draggableMask);
        if (_hit.collider != null)
        {
            if (!_isSelected)
            {
                _isSelected = true;
                //Debug.Log($"mouse on : {_hit.collider.gameObject.name}");
                OnPointerEnter();
            }

            if (_selectedCard != _hit.collider.gameObject && !_isDragging)
            {
                OnPointerExit();
                OnPointerEnter();
            }
        }
        else
        {
            if (_isSelected && !_isDragging)
            {
                _isSelected = false;
                OnPointerExit();
            }
        }

        if (_isDragging)
        {
            _selectedCard.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(_trackingAction.ReadValue<Vector2>()).x + _offsetMouse.x, Camera.main.ScreenToWorldPoint(_trackingAction.ReadValue<Vector2>()).y + _offsetMouse.y, -10);
        }
    }

    private void OnEnable()
    {
        _trackingAction.Enable();
        _clickingAction.Enable();
        _clickingAction.started += OnClickDown;
        _clickingAction.canceled += OnClickUp;
    }

    private void OnDisable()
    {
        _trackingAction.Disable();
        _clickingAction.Disable();
        _clickingAction.started -= OnClickDown;
        _clickingAction.canceled -= OnClickUp;
    }

    private void OnClickDown(InputAction.CallbackContext context)
    {
        if (_hit.collider != null)
        {
            _isDragging = true;
            _offsetMouse = _selectedCard.transform.position - Camera.main.ScreenToWorldPoint(_trackingAction.ReadValue<Vector2>());

            _startDragPosition = _selectedCard.transform.position;
            _startDragContainer = _selectedCard.GetComponentInParent<ContainerElement>();
            //Debug.Log($"click on : {_hit.collider.gameObject.name}");
        }
    }

    private void OnClickUp(InputAction.CallbackContext context)
    {
        if (_isDragging)
        {
            _isDragging = false;
            _isOnColllider = false;

            foreach (Collider2D collider in CardManager.Instance.CardContainers)
            {
                ContainerElement container = collider.GetComponentInParent<ContainerElement>();
                if (collider.OverlapPoint(new Vector2(_selectedCard.transform.position.x, _selectedCard.transform.position.y)) && !_isOnColllider && container != _startDragContainer)
                {
                    _isOnColllider = true;
                    bool isValid = container.AddCard(_selectedCard);
                    _startDragContainer.RemoveElement(_selectedCard);
                    if (!isValid) 
                    {
                        _startDragContainer.AddElement(_selectedCard);
                    }
                }
            }
            if (!_isOnColllider)
            {
                _selectedCard.transform.position = _startDragPosition;
            }
        }
    }

    private void OnPointerEnter()
    {
        _selectedCard = _hit.collider.gameObject.GetComponent<CardElement>();
        _selectedCard.transform.localScale = Vector3.one * SCALE;
    }

    private void OnPointerExit()
    {
        _selectedCard.transform.localScale = Vector3.one;
    }


}
