using UnityEngine;

public class MainGame : MonoBehaviour
{
    private CardManager _cardManager;

    private void Awake()
    {
        _cardManager = gameObject.GetComponent<CardManager>();
    }
    public void Start()
    {
        _cardManager.DeackCreation();
    }
}
