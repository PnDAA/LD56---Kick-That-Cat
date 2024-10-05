using UnityEngine;

public static class MathUtils
{
    public static int mod(int x, int m)
    {
        int r = x%m;
        return r<0 ? r+m : r;
    }

    public static float mod(float x, float m)
    {
        float r = x%m;
        return r<0 ? r+m : r;
    }

    public static float MultiplyWithFloat<T>(T value, float ratio)
    {
        return ToFloat(value) * ratio;
    }

    public static float ToFloat<T>(T value)
    {
        switch (value)
        {
            case float f:
                return f;
            case double d:
                return ((float) d);
            case int i:
                return i;
            default:
                throw new System.NotImplementedException($"ToFloat unknown type: {value.GetType()}");
        }
    }

    // https://stackoverflow.com/questions/13285007/how-to-determine-if-a-point-is-within-an-ellipse
    public static bool IsInEllipsis(Vector3 position, float radiusX, float radiusY)
    {
        /* This is a more general form of the circle equation
        *
        * X^2/a^2 + Y^2/b^2 <= 1
        */
        return ((position.x * position.x / (radiusX * radiusX)) + (position.y * position.y / radiusY / radiusY)) <= 1.0;
    }
}
