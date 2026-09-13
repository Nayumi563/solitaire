using System;
using UnityEngine;

[System.Serializable]
public class CardInfo
{
    public CardCategory Category;
    public int Number;
    [field: SerializeField] public bool IsReturn { get; private set; }
    public event Action<bool> CardIsReturnChanged;

    public CardInfo(CardCategory category, int number, bool isReturn = true)
    {
        Category = category;
        Number = number;
        IsReturn = isReturn;
    }

    public void SetIsReturn(bool isReturn)
    {
        if (IsReturn != isReturn)
        {
            CardIsReturnChanged.Invoke(isReturn);
            IsReturn = isReturn;
        }
    }
}
