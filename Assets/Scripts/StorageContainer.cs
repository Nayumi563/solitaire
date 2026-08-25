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

            if (_count != 0)
            {
                CardElements[_count - 1].gameObject.SetActive(false);
            }

            _count++;
        }
        else
        {
            resetPosition();
        }
    }

    private bool CardIsValid(CardElement element) {
        if (_count < CardManager.Instance.CardNumbers.Length && element.Info.Number == CardManager.Instance.CardNumbers[_count])
        {
            if (_count == 0) 
            {
                return true;
            }
            else if (CardElements[_count - 1].Info.Category == element.Info.Category) 
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

