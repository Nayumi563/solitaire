using System;
using UnityEngine;

public class ColumnContainer : ContainerElement
{
    private float _offset = 0.4f;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponentInChildren<BoxCollider2D>();
    }

    public override void AddElement(CardElement element)
    {
        base.AddElement(element);
        element.transform.position = CalculateCardPosition(CardElements.Count - 1);
        if (CardElements.Count > 1)
        {
            CardElements[CardElements.Count - 2].Info.SetIsReturn(true);
        }

        CardElements[CardElements.Count - 1].Info.SetIsReturn(false);
        OffsetCollider();
    }

    public override void AddElement(CardElement element, Action resetPosition)
    {
        if (CardElements.Count == 0)
        {
            base.AddElement(element, resetPosition);
            return;
        }

        if (element.gameObject.GetComponentInParent<ContainerElement>() == this)
        {
            resetPosition();
            return;
        }

        element.gameObject.GetComponentInParent<ContainerElement>().RemoveElement(element);
        element.transform.SetParent(CardElements[CardElements.Count - 1].gameObject.transform);

        CardElement[] childrenCards = element.GetComponentsInChildren<CardElement>();
        foreach (CardElement child in childrenCards)
        {
            CardElements.Add(child);
            child.transform.position = CalculateCardPosition(CardElements.Count - 1);
            Debug.Log(child.gameObject.name + child.transform.position);
        }

        OffsetCollider();
    }

    public override void RemoveElement(CardElement card)
    {
        if (CardElements.Count > 1)
        {
            CardElements[CardElements.IndexOf(card) - 1].Info.SetIsReturn(false);
        }

        CardElement[] childrenCards = card.GetComponentsInChildren<CardElement>();
        foreach (CardElement child in childrenCards)
        {
            CardElements.Remove(child);
        }
        CardElements.Remove(card);

        OffsetCollider();
    }

    private Vector3 CalculateCardPosition(int cardIndex)
    {
        return transform.position + new Vector3(0, -cardIndex * _offset, (-cardIndex - 1) * 0.1f);
    }

    private void OffsetCollider()
    {
        _collider.transform.position = new Vector3(transform.position.x, CalculateCardPosition(CardElements.Count).y);
    }
}
