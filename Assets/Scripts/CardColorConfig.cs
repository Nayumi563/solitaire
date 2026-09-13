using UnityEngine;

[CreateAssetMenu(fileName = "CardColorConfig", menuName = "Scriptable Objects/CardColorConfig")]
public class CardColorConfig : ScriptableObject
{
    public CardCategory Config;
    public Color Color1;
    public Color Color2;
}
