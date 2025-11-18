/// <summary>
/// Library of eases
/// Each function has a standard function and a method overload taking duration as a parameter
/// Each function will perform an ease on whatever values are passed through and return it
/// </summary>
public class Easings
{
    //QuadIn
    public static float QuadIn(float t) {
        return KMaths.pow(t, 2);
    }
    public static float QuadIn(float t, float d) {
        t /= d;
        return KMaths.pow(t, 2);
    }

    //QuadOut
    public static float QuadOut(float t) {
        return 1 - (1 - t) * (1 - t);
    }
    public static float QuadOut(float t, float d) {
        t /= d;
        return 1 - (1 - t) * (1 - t);
    }

    //QuadInOut
    public static float QuadInOut(float t) {
        return t < 0.5 ? 2 * t * t : 1 - KMaths.pow(-2 * t + 2, 2) / 2;
    }
    public static float QuadInOut(float t, float d) {
        t /= d;
        return t < 0.5 ? 2 * t * t : 1 - KMaths.pow(-2 * t + 2, 2) / 2;
    }

    //CubicIn
    public static float CubicIn(float t) {
        return KMaths.pow(t, 3);
    }
    public static float CubicIn(float t, float d) {
        t /=  d;
        return KMaths.pow(t, 3);
    }

    //CubicOut
    public static float CubicOut(float t) { 
        return 1 - KMaths.pow(1 - t, 3);
    }
    public static float CubicOut(float t, float d) {
        t /= d;
        return 1 - KMaths.pow(1 - t, 3);
    }

    //CubicInOut
    public static float CubicInOut(float t) {
        return t < 0.5 ? 4 * KMaths.pow(t, 3) : 1 - KMaths.pow(-2 * t + 2, 3) / 2;
    }
    public static float CubicInOut(float t, float d) {
        t /= d;
        return t < 0.5 ? 4 * KMaths.pow(t, 3) : 1 - KMaths.pow(-2 * t + 2, 3) / 2;
    }

    //QuartIn
    public static float QuartIn(float t) {
        return KMaths.pow(t, 4);
    }
    public static float QuartIn(float t, float d) {
        t /= d;
        return KMaths.pow(t, 4);
    }

    //QuartOut
    public static float QuartOut(float t) {
        return 1 - KMaths.pow(1 - t, 4);
    }
    public static float QuartOut(float t, float d) {
        t /= d;
        return 1 - KMaths.pow(1 - t, 4);
    }

    //QuartInOut
    public static float QuartInOut(float t) {
        return t < 0.5 ? 8 * KMaths.pow(t, 4) : 1 - KMaths.pow(-2 * t + 2, 4) / 2;
    }
    public static float QuartInOut(float t, float d) {
        t /= d;
        return t < 0.5 ? 8 * KMaths.pow(t, 4) : 1 - KMaths.pow(-2 * t + 2, 4) / 2;
    }

    //QuintIn
    public static float QuintIn(float t) {
        return KMaths.pow(t, 5);
    }
    public static float QuintIn(float t, float d) {
        t /= d;
        return KMaths.pow(t, 5);
    }

    //QuintOut
    public static float QuintOut(float t) {
        return 1 - KMaths.pow(1 - t, 5);
    }
    public static float QuintOut(float t, float d) {
        t /= d;
        return 1 - KMaths.pow(1 - t, 5);
    }

    //QuintInOut
    public static float QuintInOut(float t) {
        return t < 0.5 ? 16 * KMaths.pow(t, 5) : 1 - KMaths.pow(-2 * t + 2, 5) / 2;
    }
    public static float QuintInOut(float t, float d) {
        t /= d;
        return t < 0.5 ? 16 * KMaths.pow(t, 5) : 1 - KMaths.pow(-2 * t + 2, 5) / 2;
    }

    //CircIn
    public static float CircIn(float t) {
        return 1 - KMaths.sqrt(1 - KMaths.pow(t, 2));
    }
    public static float CircIn(float t, float d) {
        t /= d;
        return 1 - KMaths.sqrt(1 - KMaths.pow(t, 2));
    }

    //CircOut
    public static float CircOut(float t) {
        return KMaths.sqrt(1 - KMaths.pow(t - 1, 2));
    }
    public static float CircOut(float t, float d) {
        t /= d;
        return KMaths.sqrt(1 - KMaths.pow(t - 1, 2));
    }

    //CircInOut
    public static float CircInOut(float t) {
        return t < 0.5 ?
        (1 - KMaths.sqrt(1 - KMaths.pow(2 * t, 2))) / 2 :
        (KMaths.sqrt(1 - KMaths.pow(-2 * t + 2, 2)) + 1) / 2;
    }
    public static float CircInOut(float t, float d) {
        t /= d;
        return t < 0.5 ?
        (1 - KMaths.sqrt(1 - KMaths.pow(2 * t, 2))) / 2 :
        (KMaths.sqrt(1 - KMaths.pow(-2 * t + 2, 2)) + 1) / 2;
    }
}