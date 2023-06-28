using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// Transforms a passed angle in degrees into a vector.
    /// </summary>
    /// <param name="angleInDegrees">Angle in degrees that is transformed</param>
    /// <returns>Direction vector</returns>
    public static Vector2 DirectionFromAngle(float angleInDegrees)
    {
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}