using System;
using UnityEngine;

public class ColumnContainer : ContainerElement
{
    private int _count = 0;
    private float _offset = 0.4f;

    public override void AddElement(CardElement element)
    {
        base.AddElement(element);
        element.transform.position = CalculateCardPosition(_count);
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
            _count++;
            return;
        }

        if (element.gameObject.GetComponentInParent<ContainerElement>() == this)
        {
            resetPosition();
            return;
        }

        int removeElement = element.gameObject.GetComponentInParent<ContainerElement>().RemoveElement(element);
        element.transform.SetParent(CardElements[_count - 1].gameObject.transform);
        CardElements[_count - 1].AddChild(element);
        CardElements.Add(element);

        element.transform.position = CalculateCardPosition(_count);
        //element.Canvas.sortingOrder = _count;

        _count++;
        if (element.Childs != null) 
        {
            foreach (CardElement child in element.Childs) 
            {
                CardElements.Add(child);
                child.transform.position = CalculateCardPosition(_count);
                //child.Canvas.sortingOrder = _count;
                _count++;
            }
        }
        //CardElements[CardElements.IndexOf(element) - 1].gameObject.layer = 2;

        OffsetCollider();

    }

    public override int RemoveElement(CardElement element)
    {
        if (CardElements.Count > 1)
        {
            CardElements[CardElements.IndexOf(element) - 1].Info.SetIsReturn(false);
            CardElements[CardElements.IndexOf(element) - 1].gameObject.layer = 0;
            CardElements[CardElements.IndexOf(element) - 1].Childs = null;
        }

        int countRemoveElement = element.Childs == null ? countRemoveElement = 1 : countRemoveElement = element.Childs.Count + 1;
        Debug.Log($"count remove element column container : {countRemoveElement}");


        _count = CardElements.IndexOf(element) - 1;
        CardElements.Remove(element);
        foreach (CardElement child in element.Childs )
        {
            CardElements.Remove(child);
        }

        OffsetCollider();

        return countRemoveElement;
    }

    private Vector3 CalculateCardPosition(int cardIndex)
    {
        return transform.position + new Vector3(0, -cardIndex * _offset, (-cardIndex - 1) * 0.1f);
    }

    private void OffsetCollider()
    {
        Collider.transform.position = transform.position + new Vector3(0, CalculateCardPosition(_count).y);
    }
}
