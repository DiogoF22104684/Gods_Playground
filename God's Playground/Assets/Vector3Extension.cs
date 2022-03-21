using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
    public static Vector3 x(this Vector3 vec, float value)
    {
        return new Vector3(value, vec.y, vec.z);
    }
    public static Vector3 y(this Vector3 vec, float value)
    {
        return new Vector3(vec.x, value, vec.z);
    }
    public static Vector3 z(this Vector3 vec, float value)
    {
        return new Vector3(vec.x,vec.y,value);
    }
}
