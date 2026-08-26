using System;
using Unity.VisualScripting;
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

        element.gameObject.GetComponentInParent<ContainerElement>().RemoveElement(element);
        element.transform.SetParent(CardElements[_count - 1].gameObject.transform);
        CardElements[_count - 1].AddChild(element);
        CardElements.Add(element);

        element.transform.position = CalculateCardPosition(_count);
        Debug.Log(element.gameObject.name + element.transform.position, element.gameObject);
        Debug.Log(_count, gameObject);

        //element.Canvas.sortingOrder = _count;

        _count++;
        if (element.Childs != null) {
            // TODO: Use this technique if it doesn't break anything
            //CardElement[] childrenCards = element.GetComponentsInChildren<CardElement>();
            //foreach (CardElement child in childrenCards) {

            foreach (CardElement child in element.Childs) {
                CardElements.Add(child);
                child.transform.position = CalculateCardPosition(_count);
                Debug.Log(child.gameObject.name + child.transform.position);
                //child.Canvas.sortingOrder = _count;
                _count++;
            }
        }
        Debug.Log($"{gameObject.name} cont = {_count}");

        OffsetCollider();
    }

    public override int RemoveElement(CardElement element)
    {
        if (CardElements.Count > 1)
        {
            CardElements[CardElements.IndexOf(element) - 1].Info.SetIsReturn(false);
            //CardElements[CardElements.IndexOf(element) - 1].gameObject.layer = 0;
            CardElements[CardElements.IndexOf(element) - 1].Childs = null;
        }

        int countRemoveElement = element.Childs == null ? countRemoveElement = 1 : countRemoveElement = element.Childs.Count + 1;
        Debug.Log($"count remove element column container : {countRemoveElement}");


        _count = CardElements.IndexOf(element);
        if (element.Childs != null) {
            foreach (CardElement child in element.Childs) {
                CardElements.Remove(child);
            }
        }
        CardElements.Remove(element);

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
