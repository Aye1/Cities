using System;
using UnityEngine;

public static class VectorUtils
{

    // Should we do that with vector multiplication/masking?
    public static Vector3 XToZero(Vector3 vec)
    {
        return new Vector3(0.0f, vec.y, vec.z);
    }

    public static Vector3 YToZero(Vector3 vec)
    {
        return new Vector3(vec.x, 0.0f, vec.z);
    }

    public static Vector3 ZToZero(Vector3 vec)
    {
        return new Vector3(vec.x, vec.y, 0.0f);
    }
}
