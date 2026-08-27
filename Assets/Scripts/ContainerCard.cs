using System.Collections.Generic;
using UnityEngine;

public abstract class ContainerCard : MonoBehaviour
{
    public List<CardElement> CardElements = new();

    public virtual void AddElement(CardElement card)
    {
        CardElements.Add(card);
        card.transform.SetParent(transform);
        card.transform.position = gameObject.transform.position;
        card.transform.localScale = new Vector3(1, 1, 1);
    }

    public virtual bool AddCard(CardElement card)
    {
        AddElement(card);
        return true;
    }

    public virtual void RemoveElement(CardElement card)
    {
        CardElements.Remove(card);
    }
}
