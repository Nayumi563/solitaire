using UnityEngine;
using UnityEngine.UI;

public class PileContainer : ContainerCard 
{
    private int _count = -1;
    [SerializeField] private Button _button;

    public override void AddElement(CardElement card) 
    {
        base.AddElement(card);
        card.Info.SetIsReturn(false);
    }

    public override void RemoveElement(CardElement card) 
    {
        base.RemoveElement(card);

        if (CardElements.Count == 0)
        {
            _button.gameObject.SetActive(false);
            return;
        }

        _count--;
        UpdatePile();
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
        _count++;
        UpdatePile();
    }

    private void UpdatePile()
    {
        if (_count >= CardElements.Count)
        {
            _count = -1;

            foreach (CardElement card in CardElements)
            {
                card.gameObject.SetActive(false);
            }
            return;
        }

        if (_count > 1)
        {
            CardElements[(_count - 2)].gameObject.SetActive(false);
        }

        if (_count > 0)
        {
            CardElements[(_count - 1)].gameObject.SetActive(true);
            CardElements[(_count - 1)].transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        
        if (_count >= 0)
        {
            CardElements[_count].gameObject.SetActive(true);
            CardElements[_count].transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }
}