using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector3 ToVector3WithY0(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }

    public static Vector2 ToVector2XZ(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }
}
