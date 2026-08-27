public class StorageContainer : ContainerCard
{
    public override bool AddCard(CardElement element)
    {
        if (CardIsValid(element))
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

    private bool CardIsValid(CardElement element) {
        if (CardElements.Count - 1 < CardManager.Instance.CardNumbers.Length && element.Info.Number == CardManager.Instance.CardNumbers[CardElements.Count])
        {
            if (CardElements.Count == 0 || CardElements[CardElements.Count - 1].Info.Category == element.Info.Category) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        else 
        {
            return false;
        }
    }
}

