using UnityEngine;

public class StorageContainer : ContainerCard
{
    public override bool AddCard(CardElement element)
    {
        CardElement[] childrenCards = element.GetComponentsInChildren<CardElement>();

        if (CardElements.Count - 1 < CardManager.Instance.CardNumbers.Length && element.Info.Number - 1 == CardElements.Count && childrenCards.Length == 1 && (CardElements.Count == 0 || CardElements[CardElements.Count - 1].Info.Category == element.Info.Category))
        {
            base.AddElement(element);
            element.gameObject.layer = 2;

            if (CardElements.Count > 1)
            {
                CardElements[CardElements.Count - 2].gameObject.SetActive(false);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}

