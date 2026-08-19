using System;
using UnityEngine;

public abstract class ContainerElement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _enterCollider)
    {
        Debug.Log(_enterCollider + "in" + this);
    }

    public virtual void AddElement(CardElement element)
    {
        element.transform.SetParent(transform);
        element.transform.position = gameObject.transform.position;
        element.transform.localScale = new Vector3(1, 1, 1);
    }
    public virtual void AddElement(CardElement element, Action resetPosition)
    {
    }
}
