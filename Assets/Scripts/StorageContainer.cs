using System;

public class StorageContainer : ContainerElement
{
    private int count = 0;
    public override void AddElement(CardElement element, Action resetPosition)
    {
        if (element.Info.Number == CardManager.Instance.CardNumbers[count])
        {
            count++;
            element.transform.SetParent(gameObject.transform);
            element.transform.position = gameObject.transform.position;
            element.gameObject.layer = 2;
        }
        else
        {
            resetPosition();
        }
    }

}

