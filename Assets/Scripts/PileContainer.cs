using UnityEngine;

public class PileContainer : ContainerCard {
    private int _count = -1;
    public override void AddElement(CardElement card) {
        base.AddElement(card);
        card.Info.SetIsReturn(false);
    }

    public override void RemoveElement(CardElement card) {
        if (CardElements.IndexOf(card) > 1) {
            CardElements[CardElements.IndexOf(card) - 2].gameObject.SetActive(true);
            CardElements[CardElements.IndexOf(card) - 2].transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            CardElements[CardElements.IndexOf(card) - 1].transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        _count--;
        base.RemoveElement(card);
    }

    private void Start() {
        foreach (CardElement card in CardElements) {
            card.gameObject.SetActive(false);
        }
    }

    public void OnClickPile() {
        if (_count >= 1) 
        {
            CardElements[(_count - 1) % CardElements.Count].gameObject.SetActive(false);
        }
        if (_count >= 0) 
        {
            CardElements[_count % CardElements.Count].transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        _count++;
        CardElements[_count % CardElements.Count].gameObject.SetActive(true);
        CardElements[_count % CardElements.Count].transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

}