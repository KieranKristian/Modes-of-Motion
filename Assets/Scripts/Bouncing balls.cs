using System.Collections;
using UnityEngine;

public class Bouncingballs : MonoBehaviour
{
    GameObject[] Balls;
    float force = 20f;
    
    private void Start() {
        //Fills the game object array with any object with the "Ball" tag
        Balls = GameObject.FindGameObjectsWithTag("Ball");
        StartCoroutine(Bounce());
    }

    /// <summary>
    /// Loops through all of the balls in the array, creates a random direction for the balls to go and adds force in that direction
    /// </summary>
    IEnumerator Bounce() {
        while (true) {
            foreach (GameObject Ball in Balls) {
                Vector3 Direction = transform.up + new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f));
                Ball.GetComponent<Rigidbody>().AddForce(Direction * force);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
