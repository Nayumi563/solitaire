using UnityEngine;

public class Colors : MonoBehaviour
{
    [SerializeField] private Color _darkPurple;
    static public Color DarkPurple;
    public ColorConfig[] colorConfigs;
    private void Start()
    {
        DarkPurple = _darkPurple;
    }
}
[System.Serializable]
public class ColorInfo
{
    public string Name;
    public Color Color;
    ColorInfo(string name, Color color)
    {
        Name = name;
        Color = color;
    }
}
[System.Serializable]
public class ColorConfig
{
    public string Name;
    public ColorInfo ColorInfo;
    public ColorInfo ColorInfo2;

}
