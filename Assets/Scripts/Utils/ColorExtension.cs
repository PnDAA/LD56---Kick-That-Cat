using UnityEngine;

public static class ColorExtension
{
    public static Color WithAlpha(Color color, float a)
    {
        return new Color(color.r, color.g, color.b, a);
    }
}
