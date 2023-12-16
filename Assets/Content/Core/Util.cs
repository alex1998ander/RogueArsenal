using System;
using System.Collections;
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

    /// <summary>
    /// Default coroutine for a start and end action that are delayed by a certain time
    /// </summary>
    /// <param name="actionOn">start action</param>
    /// <param name="actionOff">end action</param>
    /// <param name="delayInSec">delay in seconds</param>
    /// <returns></returns>
    public static IEnumerator OnOffCoroutine(Action actionOn, Action actionOff, float delayInSec)
    {
        actionOn?.Invoke();
        yield return new WaitForSeconds(delayInSec);
        actionOff?.Invoke();
    }
}