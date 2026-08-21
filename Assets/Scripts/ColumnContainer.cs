using System;
using UnityEngine;

public class ColumnContainer : ContainerElement
{
    private int _count = 0;
    private float _offset = 0.4f;

    public override void AddElement(CardElement element)
    {
        base.AddElement(element);
        element.transform.position = CalculateOffset(_count);
        if (_count != 0)
        {
            CardElements[_count - 1].Info.SetIsReturn(true);
        }

        CardElements[_count].Info.SetIsReturn(false);
        OffsetCollider();

        _count++;
    }

    public override void AddElement(CardElement element, Action resetPosition)
    {
        element.gameObject.GetComponentInParent<ContainerElement>().RemoveElement(element);
        element.transform.SetParent(transform);
        CardElements.Add(element);
        element.transform.position = CalculateOffset(_count);

        if (_count != 0)
        {
            CardElements[_count - 1].gameObject.layer = 2;
        }

        OffsetCollider();

        _count++;
    }

    public override void RemoveElement(CardElement element)
    {
        base.RemoveElement(element);
        _count--;
        if (_count > 0)
        {
            CardElements[_count - 1].Info.SetIsReturn(false);
            CardElements[_count - 1].gameObject.layer = 0;
        }

        OffsetCollider();
    }

    private Vector3 CalculateOffset(int index)
    {
        return gameObject.transform.position + new Vector3(0, -index * _offset, (-index - 1) * 0.1f);
    }

    private void OffsetCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, CalculateOffset(_count).y * 5); // TODO: Calculate better offset
    }
}
