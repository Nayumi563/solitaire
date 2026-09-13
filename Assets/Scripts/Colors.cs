using UnityEngine;

public class Colors : MonoBehaviour
{
    public CardColorConfig[] ColorConfig;

    public CardColorConfig GetColorConfig(CardCategory config)
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