
using Unity.VisualScripting;

public class PileContainer : ContainerElement
{
    private int count = 0;
    public override void AddElement(CardElement card)
    {
        base.AddElement(card);
        card.Info.SetIsReturn(false);
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
        if (CardElements.Count > 0) 
        {
            CardElements[count % CardElements.Count].gameObject.SetActive(false);
            count++;
            CardElements[count % CardElements.Count].gameObject.SetActive(true);
        }
    }

}