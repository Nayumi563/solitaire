using UnityEngine;

public class ColumnContainer : ContainerCard
{
    private const float OFFSET = 0.4f;
    private float _offset;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponentInChildren<BoxCollider2D>();
        _offset = OFFSET;
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

    public override bool AddCard(CardElement card)
    {
        if (CardElements.Count != 0)
        {
            if (card.Info.Number != CardElements[CardElements.Count - 1].Info.Number - 1
            || card.CardIsRed() == CardElements[CardElements.Count - 1].CardIsRed())
            {
                return false;
            }
        }
        else if (card.Info.Number != 13)
        {
            return false;
        }
        
        
        if (CardElements.Count == 0)
        {
            card.transform.SetParent(transform);
        }
        else
        {
            card.transform.SetParent(CardElements[CardElements.Count - 1].gameObject.transform);
        }

        CardElement[] childrenCards = card.GetComponentsInChildren<CardElement>();
        foreach (CardElement child in childrenCards)
        {
            CardElements.Add(child);

            child.transform.position = CalculateCardPosition(CardElements.Count - 1);
            Debug.Log(child.gameObject.name + child.transform.position);
        }

        OffsetCollider();

        return true;
    }

    public override void RemoveElement(CardElement card)
    {
        if (CardElements.IndexOf(card) > 0)
        {
            CardElements[CardElements.IndexOf(card) - 1].Info.SetIsReturn(false);
        }

        CardElement[] childrenCards = card.GetComponentsInChildren<CardElement>();
        foreach (CardElement child in childrenCards)
        {
            CardElements.Remove(child);
        }
        CalculateCardPosition(CardElements.Count - 1);
        OffsetCollider();
    }

    private Vector3 CalculateCardPosition(int cardIndex)
    {
        _offset = CardElements.Count > 8 ? 8 * OFFSET / CardElements.Count : _offset = OFFSET;

        foreach (CardElement card in CardElements)
        {
            card.transform.position = transform.position + new Vector3(0, -CardElements.IndexOf(card) * _offset, (-CardElements.IndexOf(card) - 1) * 0.1f);
            Debug.Log(card.gameObject.name + card.transform.position);
        }
        return transform.position + new Vector3(0, -cardIndex * _offset, (-cardIndex - 1) * 0.1f);
    }

    private void OffsetCollider()
    {
        _collider.transform.position = new Vector3(transform.position.x, CalculateCardPosition(CardElements.Count).y);
    }
}
