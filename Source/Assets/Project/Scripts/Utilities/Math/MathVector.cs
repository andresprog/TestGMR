using UnityEngine;

/// <summary>
/// https://docs.unity3d.com/es/current/Manual/UnderstandingVectorArithmetic.html
/// </summary>
public class MathVector : MonoBehaviour
{
    public static Vector3 __CalculateUnitVector(Vector3 vector)
    {
        return vector.normalized;
    }
    public static Vector2 __CalculateUnitVector(Vector2 vector)
    {
        return vector.normalized;
    }
    public static float __CalculateMagnitude(Vector3 vector)
    {
        return vector.magnitude;
    }
    public static float __CalculateMagnitude(Vector2 vector)
    {
        return vector.magnitude;
    }

    // Parallel Vector
    // https://www.geogebra.org/m/vDyfH74J
    public static Vector3 __CalculateParallelVector(Vector3 vector, float delta)
    {
        //Vector3.
        return vector.normalized;
    }
}
