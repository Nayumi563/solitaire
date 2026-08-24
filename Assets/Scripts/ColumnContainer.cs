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
        if (_count == 0)
        {
            base.AddElement(element, resetPosition);
            return;
        }

        if (element.gameObject.GetComponentInParent<ContainerElement>() == this)
        {
            resetPosition();
            return;
        }

        int removeElement = element.gameObject.GetComponentInParent<ContainerElement>().RemoveElement(element);
        element.transform.SetParent(CardElements[_count - 1].gameObject.transform);
        //CardElements[_count - 1].gameObject.GetComponent<BoxCollider2D>().size = new Vector2(CardElements[_count - 1].gameObject.GetComponent<BoxCollider2D>().size.x, _offset);
        CardElements.Add(element);
        element.transform.position = CalculateOffset(_count);


        if (_count != 0)
        {
            CardElements[CardElements.IndexOf(element) - 1].gameObject.layer = 2;
        }

        OffsetCollider();

        _count += removeElement;
    }

    public override int RemoveElement(CardElement element)
    {
        if (CardElements.Count > 1)
        {
            CardElements[CardElements.IndexOf(element) - 1].Info.SetIsReturn(false);
            CardElements[CardElements.IndexOf(element) - 1].gameObject.layer = 0;
        }

        int countRemoveElement = _count - CardElements.IndexOf(element);
        Debug.Log(countRemoveElement);

        _count = CardElements.IndexOf(element);
        //if (CardElements.IndexOf(element) != _count - 1)
        //{
        //    // remove element on top of index element in list
        //    for (int i = CardElements.Count; i > CardElements.IndexOf(element); i--)
        //    {
        //        countRemoveElement++;
        //        Debug.Log(countRemoveElement);
        //        CardElements.RemoveAt(i);
        //    }
        //}
        CardElements.Remove(element);

        OffsetCollider();

        return countRemoveElement;
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
