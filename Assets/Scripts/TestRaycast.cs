using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestRaycast : MonoBehaviour
{
    private bool _isDragging;
    private bool _isSelected;
    [SerializeField] private LayerMask _draggableMask;
    private InputSystem_Actions _inputActions;
    private InputAction _trackingAction;
    private InputAction _clickingAction;
    private RaycastHit2D _hit;
    private GameObject _selectedObject;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _trackingAction = _inputActions.DragAndDrop.Tracking;
        _clickingAction = _inputActions.DragAndDrop.Clicking;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(_trackingAction.ReadValue<Vector2>().x, _trackingAction.ReadValue<Vector2>().y, Camera.main.transform.position.z * 100f));
        _hit = Physics2D.Raycast(ray.origin, ray.direction, 200000, _draggableMask);
        if (_hit.collider != null)
        {
            if (!_isSelected)
            {
                _isSelected = true;
                //Debug.Log($"mouse on : {_hit.collider.gameObject.name}");
                _selectedObject = _hit.collider.gameObject;
                _selectedObject.GetComponent<DragAndDrop>().OnPointerEnter();
            }

            if (_selectedObject != _hit.collider.gameObject && !_isDragging) {
                _selectedObject.GetComponent<DragAndDrop>().OnPointerExit();
                _selectedObject = _hit.collider.gameObject;
                _selectedObject.GetComponent<DragAndDrop>().OnPointerEnter();
            }

            if (_isDragging)
            {
                _selectedObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(_trackingAction.ReadValue<Vector2>()).x, Camera.main.ScreenToWorldPoint(_trackingAction.ReadValue<Vector2>()).y, -10);
            }
        }
        else
        {
            if (_isSelected)
            {
                _isSelected = false;
                _selectedObject.GetComponent<DragAndDrop>().OnPointerExit();
            }
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
            _selectedObject.GetComponent<DragAndDrop>().OnPointerDown();
            //Debug.Log($"click on : {_hit.collider.gameObject.name}");
        }
    }

    private void OnClickUp(InputAction.CallbackContext context)
    {
        if (_isDragging)
        {
            _isDragging = false;
            _selectedObject.GetComponent<DragAndDrop>().OnPointerUp();
        }
    }
}
