using System.Collections;
using UnityEngine;

public class DangerMeterScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Flick());
    }

    /// <summary>
    /// Adds force to the object every 0.2 seconds
    /// </summary>
    IEnumerator Flick() {
        while (true) {
            GetComponent<Rigidbody>().AddForce(0,0,1000);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
