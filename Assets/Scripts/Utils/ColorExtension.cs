using UnityEngine;

public static class ColorExtension
{
    public static Color WithAlpha(this Color color, float a)
    {
        return new Color(color.r, color.g, color.b, a);
    }
}
