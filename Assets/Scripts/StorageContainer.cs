using System;

public class StorageContainer : ContainerElement
{
    private int count = 0;
    public override void AddElement(CardElement element, Action resetPosition)
    {
        if (count < CardManager.Instance.CardNumbers.Length && element.Info.Number == CardManager.Instance.CardNumbers[count])
        {
            base.AddElement(element, resetPosition);
            element.gameObject.layer = 2;

            if (count != 0)
            {
                CardElements[count - 1].gameObject.SetActive(false);
            }

            count++;
        }
        else
        {
            resetPosition();
        }
    }

}

