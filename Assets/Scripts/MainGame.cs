using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private string[] ColorNames;
    public string[] ColorNameKeys;
    public Dictionary<string, Color> ColorsDictionary = new Dictionary<string, Color>();

    private CardManager _cardManager;
    public Colors Colors;

    public void Awake()
    {
        ColorNameKeys = ColorNames;
        _cardManager = gameObject.GetComponent<CardManager>();
        Colors = gameObject.GetComponent<Colors>();
        if (_colors.Length != ColorNames.Length)
        {
            Debug.Log("color set up error");
        }
        else
        {
            for (int i = 0; i < _colors.Length-1; i++)
            {
                ColorsDictionary.Add(ColorNames[i], _colors[i]);
            }
        }
    }
    public void Start()
    {
        _cardManager.DeackCreation();
    }


}
