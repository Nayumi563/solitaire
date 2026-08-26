using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 _startDragPosition;
    private const float SCALE = 1.1f;
    private bool _isOnColllider;
    private CardElement _cardElement;

    private void Awake()
    {
        _cardElement = gameObject.GetComponent<CardElement>();
    }

    public void OnPointerEnter()
    {
        transform.localScale = Vector3.one * SCALE;
    }

    public void OnPointerExit()
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerDown()
    {
        _startDragPosition = transform.position;

    }

    public void OnPointerUp()
    {
        _isOnColllider = false;

        foreach (Collider2D collider in CardManager.Instance.CardContainers)
        {
            if (collider.OverlapPoint(new Vector2(transform.position.x, transform.position.y)) && !_isOnColllider)
            {
                _isOnColllider = true;
                collider.gameObject.GetComponentInParent<ContainerElement>().AddElement(_cardElement, ResetPosition);
            }
        }
        if (!_isOnColllider)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = _startDragPosition;
    }
}
