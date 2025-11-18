using System.Collections;
using UnityEngine;

public class Camerashake : MonoBehaviour
{
    Lerping lerpScript;
    Vector3 startPosition;
    AudioSource explosionNoise;

    bool shouldShake;

    private void Start() {
        //Sets all of the variables
        lerpScript = GetComponent<Lerping>();
        explosionNoise = GetComponent<AudioSource>();
        startPosition = transform.localPosition;
        lerpScript.positionLerp = startPosition; //Makes it so the lerp starts from the cameras position instead of (0,0,0)
    }

    /// <summary>
    /// Constantly lerps the end values for the cameras position
    /// </summary>
    IEnumerator CameraLerp() {
        while (shouldShake) {
            lerpScript.Position.startValues = startPosition;  //Makes it so that the lerp starts at the position of the camera, so it doesn't start at (0,0,0)
            Vector3 randomAddition = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            Vector3 randomPoint = startPosition + randomAddition; //Adds the random Vector3 to the start position and sets the end point of the lerp to this new position
            lerpScript.Position.endValues = randomPoint;
            yield return new WaitForSeconds(0.2f);
        }
    }

    /// <summary>
    /// Sets shouldLerp to true so that the cameras position should be affected by the lerp for 2 seconds, then resets the position
    /// </summary>
    public IEnumerator StartShake() {
        //Starting shake
        shouldShake = true;
        lerpScript.enabled = true;
        if (explosionNoise != null) {
            explosionNoise.Play();
        }
        lerpScript.shouldLerp = true;
        yield return new WaitForSeconds(2f);
        //Ending shake
        lerpScript.shouldLerp = false;
        lerpScript.enabled = false;
        transform.localPosition = startPosition;
        shouldShake = false;
    }

    /// <summary>
    /// Function that can be called publicly, I.E with a button press
    /// </summary>
    public void Shake() {
        StartCoroutine(StartShake());
        StartCoroutine(CameraLerp());
    }
}
