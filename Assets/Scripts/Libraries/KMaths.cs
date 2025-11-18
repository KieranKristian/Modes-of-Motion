using System;
using UnityEngine;

public class KMaths
{
    /// <summary>
    /// Struct with Vector 2 start and end point variables
    /// </summary>
    [Serializable]
    public struct Vector2s {
        public Vector2 startValues;
        public Vector2 endValues;
    }
    /// <summary>
    /// Struct with Vector 3 start and end point variables
    /// </summary>
    [Serializable]
    public struct Vector3s {
        public Vector3 startValues;
        public Vector3 endValues;
    }
    /// <summary>
    /// Struct with Vector 4 start and end point variables
    /// </summary>
    [Serializable]
    public struct Vector4s {
        public Vector4 startValues;
        public Vector4 endValues;
    }
    /// <summary>
    /// Struct with Quaternion start and end point variables
    /// </summary>
    [Serializable]
    public struct Quaternions {
        public Quaternion startValues;
        public Quaternion endValues;
    }
    /// <summary>
    /// Struct with Colour start and end point variables
    /// </summary>
    [Serializable]
    public struct Colors {
        public Color startValues;
        public Color endValues;
    }
    /// <summary>
    /// Struct with variables necessary to create a curve
    /// </summary>
    [Serializable]
    public struct Curves {
        [Header("Positions")]
        [Tooltip("Starting Position")]
        public Vector3 aPos;
        [Tooltip("End Position")]
        public Vector3 bPos;
        [Tooltip("Point That Line Curves Towards")]
        public Vector3 cPos;
        [Tooltip("Tightness of the curve(Cardinal Spline Only)")]
        public float a;
    }

    //Method overloading for Lerps based on different parameters that are passed through
    public static float Lerp(float startValue, float endValue, float t) {
        return (startValue + (endValue - startValue) * t);
    }
    public static Vector2 Lerp(Vector2 startValues, Vector2 endValues, float t) {
        return (startValues + (endValues - startValues) * t);
    }
    public static Vector3 Lerp(Vector3 startValues, Vector3 endValues, float t) {
        return (startValues + (endValues - startValues) * t);
    }
    public static Vector4 Lerp(Vector4 startValues, Vector4 endValues, float t) {
        return (startValues + (endValues - startValues) * t);
    }

    /// <summary>
    /// Lerps between start point and curve point
    /// Lerps between end point and curve point
    /// Lerps between the two points and returns that
    /// </summary>
    public static Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, float t) {
        Vector3 mid1 = Lerp(a, b, t);
        Vector3 mid2 = Lerp(b, c, t);

        return Lerp(mid1, mid2, t);
    }

    /// <param name="a">Starting point of the curve</param>
    /// <param name="b">End point of the curve</param>
    /// <param name="c">Point that the line curves towards</param>
    /// <param name="x">'Weight/pull' of the point 'c'</param>
    /// <param name="t">How far along the lerp is</param>
    /// <returns>Vector3 point which the cube should be at</returns>
    public static Vector3 CardinalSpline(Vector3 a, Vector3 b, Vector3 c , float x, float t) {
        Vector3 tangent0 = (b - a) * x;
        Vector3 tangent1 = (c - b) * x;

        float tsq = t * t;
        float tcub = tsq * t;

        float h00 = 2 * tcub - 3 * tsq + 1;
        float h01 = -2 * tcub + 3 * tsq;
        float h10 = tcub - 2 * tsq + t;
        float h11 = tcub - tsq;

        Vector3 point = h00 * a + h10 * tangent0 + h01 * b + h11 * tangent1;
        return point;
    }

    /// <summary>
    /// Takes a float 'a' and a float 'x' as a parameter, returns the calculation of 'a' to the power of 'x'
    /// </summary>
    public static float pow(float a, float pow) {
        float x = 1;
        for (int i = 0; i < pow; i++) {
            x *= a;
        }
        return x;
    }
        
    /// <summary>
    /// Takes a float 'a' as a parameter, returns the square root of 'a'
    /// </summary>
    public static float sqrt(float a) {
        return Mathf.Sqrt(a);
    }
}