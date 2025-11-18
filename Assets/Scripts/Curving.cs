using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Curving : MonoBehaviour
{
    //Creates a struct from KMaths with all the variables necessary for making a curve
    public KMaths.Curves Curve;

    //Values which will be changed by the Lerp
    Vector3 bezierLerp;
    Vector3 cardinalLerp;

    bool lerping;
    bool shouldLoop;

    //Enum of the type of curve
    public enum Curves {
        Bezier, CardinalSpline
    }
    public Curves curve;

    //Enum of all of the easings that can be used
    public enum eases {
        QuadIn, QuadOut, QuadInOut,
        CubicIn, CubicOut, CubicInOut,
        QuartIn, QuartOut, QuartInOut,
        QuintIn, QuintOut, QuintInOut,
        CircIn, CircOut, CircInOut
    }
    public eases ease;

    public float duration = 1;

    /// <summary>
    /// Function that will iterate each frame and perform a lerp using an easing on the vector3's 
    /// </summary>
    IEnumerator CurveCalc() {
        shouldLoop = false;
        lerping = true;
        float time = 0;
        float t = 0;
        while (time < duration) {
            lerping = true;
            t = SwitchEase(time);
            
            bezierLerp = KMaths.Bezier(Curve.aPos, Curve.cPos, Curve.bPos, t);
            cardinalLerp = KMaths.CardinalSpline(Curve.aPos, Curve.bPos, Curve.cPos, Curve.a, t);
            
            time += Time.deltaTime;
            yield return null;
        }
        lerping = false;
        shouldLoop = true;
    }

    void Update() {
        if (shouldLoop) {
           //Swaps the start and end point for the curve so it loops
            Vector3 Filler = Curve.aPos;
            Curve.aPos = Curve.bPos;
            Curve.bPos = Filler;
        }
        if (!lerping) {
            StartCoroutine(CurveCalc());
        }

        //Sets the position of the object to the values affected by the lerp depending on which curve is selected with the enum
        switch (curve) {
            case Curves.Bezier:
                transform.localPosition = bezierLerp;
                break;

            case Curves.CardinalSpline:
                transform.localPosition = cardinalLerp;
                break;

            default:
                break;
        }
    }

    //Public methods which can be called by UI elements such as sliders and dropdown menus, this will change the calculations performed in the lerp
    public void DropDownMenuChoice(int choice) {
        ease = (eases)choice;
    }

    public void OnSliderValueUpdate(Slider slider) {
        duration = slider.value;
    }

    /// <summary>
    /// Function that switches the ease type based on the enum, and calculates the ease using time as a parameter and the public variable duration
    /// </summary>
    public float SwitchEase(float time) {
        return ease switch {
            eases.QuadIn => Easings.QuadIn(time, duration),
            eases.QuadOut => Easings.QuadOut(time, duration),
            eases.QuadInOut => Easings.QuadInOut(time, duration),
            eases.CubicIn => Easings.CubicIn(time, duration),
            eases.CubicOut => Easings.CubicOut(time, duration),
            eases.CubicInOut => Easings.CubicInOut(time, duration),
            eases.QuartIn => Easings.QuartIn(time, duration),
            eases.QuartOut => Easings.QuartOut(time, duration),
            eases.QuartInOut => Easings.QuartInOut(time, duration),
            eases.QuintIn => Easings.QuintIn(time, duration),
            eases.QuintOut => Easings.QuintOut(time, duration),
            eases.QuintInOut => Easings.QuintInOut(time, duration),
            eases.CircIn => Easings.CircIn(time, duration),
            eases.CircOut => Easings.CircOut(time, duration),
            eases.CircInOut => Easings.CircInOut(time, duration),
            _ => 0
        };
    }
}
