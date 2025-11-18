using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lerping : MonoBehaviour {
    //Structs that appear on each object which allows you to change the start and end values for the lerp
    public KMaths.Vector3s Position;
    public KMaths.Vector3s Rotation;
    public KMaths.Vector3s Scale;
    public KMaths.Colors ObjectColor;

    //Values which will be changed by the Lerp
    [HideInInspector] public Vector3 positionLerp;
    [HideInInspector] public Vector3 rotationLerp;
    [HideInInspector] public Vector3 scaleLerp;
    [HideInInspector] public Vector4 colorLerp;

    bool lerping; //checks whether the lerp is already happening
    bool shouldLoop;

    //Changeable variables
    public float duration = 1;
    public bool doesMove;
    public bool doesRotate;
    public bool doesScale;
    public bool doesChangeColor;

    //A switch for whether something should lerp, default to true
    public bool shouldLerp = true;

    //Enum of all of the easings that can be used
    public enum eases {
        QuadIn, QuadOut, QuadInOut, 
        CubicIn, CubicOut, CubicInOut, 
        QuartIn, QuartOut, QuartInOut,
        QuintIn, QuintOut, QuintInOut,
        CircIn, CircOut, CircInOut
    }
    public eases ease;

    /// <summary>
    /// Function that will iterate each frame and perform a lerp using an easing on the vector3's and the colour
    /// </summary>
    IEnumerator Lerps() {
        shouldLoop = false;
        lerping = true;
        float time = 0;
        float t;
        while (time < duration) {
            lerping = true;
            //Ease Functions
            t = SwitchEase(time);

            //Lerping the values
            positionLerp = KMaths.Lerp(Position.startValues, Position.endValues, t);
            rotationLerp = KMaths.Lerp(Rotation.startValues, Rotation.endValues, t);
            scaleLerp = KMaths.Lerp(Scale.startValues, Scale.endValues, t);
            colorLerp = KMaths.Lerp(ObjectColor.startValues, ObjectColor.endValues, t);

            //Incrementing
            time += Time.deltaTime;
            yield return null;
        }
        lerping = false;
        shouldLoop = true;
    }

    /// <summary>
    /// Starts the coroutine if shouldLerp is set to true
    /// Checks whether shouldLoop is true (shouldLoop is set to true when the lerp is finished), it then swaps the start and end values
    /// Checks whether the Booleans for each type of lerp are set to true, and if so, sets the position/rotation/scale/colour to the lerped values
    /// </summary>
    private void Update() {
        if (shouldLerp) {
            if (shouldLoop) {
                //Swapping start and end values so that the lerp will loop
                Vector3 posFiller = Position.startValues;
                Position.startValues = Position.endValues;
                Position.endValues = posFiller;

                Vector3 rotationFiller = Rotation.startValues;
                Rotation.startValues = Rotation.endValues;
                Rotation.endValues = rotationFiller;

                Vector3 scaleFiller = Scale.startValues;
                Scale.startValues = Scale.endValues;
                Scale.endValues = scaleFiller;

                Vector4 colorFiller = ObjectColor.startValues;
                ObjectColor.startValues = ObjectColor.endValues;
                ObjectColor.endValues = colorFiller;
            }
            if (!lerping) {
                StartCoroutine(Lerps());
            } 
        }

        if (doesMove) { 
            transform.localPosition = positionLerp;
        }
        if (doesRotate) {
            transform.localEulerAngles = rotationLerp;
        }
        if (doesScale) {
            transform.localScale = scaleLerp;
        }
        if (doesChangeColor) {
            Image image = GetComponent<Image>();
            if (image != null) {
                image.color = colorLerp;
            }
            TMP_Text text = GetComponent<TMP_Text>();
            if (text != null) {
                text.color = colorLerp;
            }
            Light light = GetComponent<Light>();
            if (light != null) {
                light.color = colorLerp;
            }
            Renderer material = GetComponent<Renderer>();
            if (material != null) {
                material.material.color = colorLerp;
            }
        }
    }

    //Public methods which can be called by UI elements such as toggles, sliders and dropdown menus, this will change the calculations performed in the lerp
    public void toggleMoving(bool toggle) {
        doesMove = toggle;
    }
    public void toggleRotating(bool toggle) {
        doesRotate = toggle;
    }
    public void toggleScaling(bool toggle) {
        doesScale = toggle;
    }
    public void toggleColoring(bool toggle) {
        doesChangeColor = toggle;
    }

    public void OnSliderValueUpdate(Slider slider) {
        duration = slider.value;
    }

    public void DropDownMenuChoice(int choice) {
        ease = (eases)choice;
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