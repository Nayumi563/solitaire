using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContainerElement : MonoBehaviour
{
    public List<CardElement> CardElements = new();

    public virtual void AddElement(CardElement element)
    {
        CardElements.Add(element);
        element.transform.SetParent(transform);
        element.transform.position = gameObject.transform.position;
        element.transform.localScale = new Vector3(1, 1, 1);
    }

    public virtual void AddElement(CardElement element, Action resetPosition)
    {
        if (element.gameObject.GetComponentInParent<ContainerElement>() == this)
        {
            resetPosition();
            return;
        }

        element.gameObject.GetComponentInParent<ContainerElement>().RemoveElement(element);
        AddElement(element);
    }

    public virtual int RemoveElement(CardElement element)
    {
        CardElements.Remove(element);
        return 1;
    }
}
