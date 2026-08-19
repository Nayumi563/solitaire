using System;
using System.Collections.Generic;
using UnityEngine;

public class PileContainer : ContainerElement
{
    private List<CardElement> _cards = new();
    private int count = 0;

    public override void AddElement(CardElement element)
    {
        base.AddElement(element);
        element.Info.SetIsReturn(false);
        _cards.Add(element);
    }

    private void Start()
    {
        foreach (CardElement card in _cards)
        {
            card.gameObject.SetActive(false);
        }
    }

    public void OnClickPile()
    {
        _cards[count % _cards.Count].gameObject.SetActive(false);
        count++;
        _cards[count % _cards.Count].gameObject.SetActive(true);
    }

}