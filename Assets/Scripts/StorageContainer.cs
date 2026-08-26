using System;

public class StorageContainer : ContainerElement
{
    private int _count = 0;
    public override void AddElement(CardElement element, Action resetPosition)
    {
        if (CardIsValid(element))
        {
            base.AddElement(element, resetPosition);
            element.gameObject.layer = 2;

            if (CardElements.Count > 1)
            {
                CardElements[CardElements.Count - 2].gameObject.SetActive(false);
            }

            _count++;
        }
        else
        {
            resetPosition();
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

