using UnityEngine;

public class Colors : MonoBehaviour
{
    [SerializeField] public CardColorConfig[] ColorConfig;
    public CardColorConfig GetColorConfig(CardConfig config)
    {
        foreach (CardColorConfig colorConfig in ColorConfig)
        {
            if (colorConfig.Config == config)
            {
                return colorConfig;
            }
        }
        Debug.Log("error set up colorconfig");
        return null;
    }
}

[CreateAssetMenu(fileName = "CardColorConfig", menuName = "Scriptable Objects/CardColorConfig")]
public class CardColorConfig : ScriptableObject
{
    public CardConfig Config;
    public Color Color1;
    public Color Color2;
}

