
using System.Collections.Generic;

public class PileContainer : ContainerElement
{
    private int count = 0;

    public override void AddElement(CardElement element)
    {
        base.AddElement(element);
        element.Info.SetIsReturn(false);
    }

    private void Start()
    {
        foreach (CardElement card in CardElements)
        {
            card.gameObject.SetActive(false);

        }
    }

    public void OnClickPile()
    {
        CardElements[count % CardElements.Count].gameObject.SetActive(false);
        count++;
        CardElements[count % CardElements.Count].gameObject.SetActive(true);
    }

}