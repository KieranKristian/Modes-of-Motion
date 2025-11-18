using System.Collections;
using UnityEngine;
using TMPro;
using StarterAssets;

public class PlayerInteractionScript : MonoBehaviour
{
    //Variables needed for the interactions with the control panels
    private Camera cam;
    public StarterAssetsInputs screenCursor;
    public FirstPersonController firstPersonController;
    bool panelActive;

    //Variables needed for turning the robot on and off
    public GameObject screen;
    public GameObject button;
    public AudioSource onOffNoise;
    public TMP_Text displayText;
    bool robotOn;
    
    //Variables used in the camera shake
    public GameObject missile;
    public Camerashake cameraShakeScript;

    bool canInteract = true;
    
    void Start()
    {
        cam = Camera.main;    
        missile.SetActive(false);
    }

    //Checks for inputs when the robot is turned on, and does different things based on what the ray cast hits
    void Update()
    {
        if (canInteract) {
            if (Input.GetKeyDown(KeyCode.E)) {
                StartCoroutine(robotActivation());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetMouseButtonDown(0)) {
            if (robotOn && !panelActive) {
                RaycastHit hit;
                if (canInteract) {
                    if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 30f)) {
                        Debug.Log("Shot " + hit.transform.name);
                        if (hit.transform.TryGetComponent(out Lerping lerpScript)) {
                            StartCoroutine(textChange(lerpScript));
                        }
                        if (hit.transform.CompareTag("Control Panel")) {
                            Debug.Log("Hit control Panel");
                            StartCoroutine(ActivatePanel(true, hit.transform));
                        }
                        if (hit.transform.CompareTag("CameraShakeButton")) {
                            StartCoroutine(MissileLaunch());
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Lerps the camera and unhides the mouse so that the player can interact with UI elements on a control panel
    /// </summary>
    /// <param name="isItActive">Takes a bool as a parameter to determine what to activate and deactivate</param>
    /// <param name="panel"></param>
    /// <returns></returns>
    IEnumerator ActivatePanel(bool isItActive, Transform panel) {
        Camera panelCam = panel.Find("PanelCam").GetComponent<Camera>();
        panelActive = isItActive;
        Cursor.visible = isItActive;
        firstPersonController.enabled = !isItActive;
        screenCursor.cursorInputForLook = !isItActive;
        if (isItActive) {
            Cursor.lockState = CursorLockMode.None;
            panelCam.enabled = true;
            StartCoroutine(OnAndOff(panelCam.GetComponent<Lerping>(), 0.1f));
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine(OnAndOff(panelCam.GetComponent<Lerping>(), 0.1f));
            yield return new WaitForSeconds(1);
            panelCam.enabled = false;
        }
    }

    /// <summary>
    /// Coroutine which will activate all of the necessary lerps for the robot when it is being turned on or off
    /// </summary>
    IEnumerator robotActivation() {
        canInteract = false;
        onOffNoise.Play();
        StartCoroutine(OnAndOff(screen.GetComponent<Lerping>(), 0.1f));
        StartCoroutine(OnAndOff(button.GetComponent<Lerping>(), 0.1f));
        StartCoroutine(OnAndOff(displayText.GetComponent<Lerping>(), 0.1f));
        yield return new WaitForSeconds(1.9f);
        canInteract = true;
        robotOn = robotOn ? false : true; //Changes the state of robotOn depending on what it is already set to
    }

    /// <summary>
    /// This will find the easing used by the object clicked on
    /// It will change the text on the robot to the easing used and will lerp the transparency of the text using the easing of the object
    /// </summary>
    IEnumerator textChange(Lerping currentEase) {
        canInteract = false;
        onOffNoise.pitch = Random.Range(1f, 1.5f);
        onOffNoise.Play();
        string easeName = currentEase.ease.ToString();
        displayText.text = easeName;
        displayText.GetComponent<Lerping>().ease = currentEase.ease;
        StartCoroutine(OnAndOff(displayText.GetComponent<Lerping>(), 1.1f));
        yield return new WaitForSeconds(2);
        canInteract = true;
        yield return null;
    }

    /// <summary>
    /// Function used to activate a lerp
    /// 0.1 delay will do the lerp once
    /// 1.1 delay will do the lerp twice
    /// </summary>
    IEnumerator OnAndOff(Lerping lerpScript, float delay) {
        lerpScript.shouldLerp = true;
        yield return new WaitForSeconds(delay);
        lerpScript.shouldLerp = false;
    }

    /// <summary>
    /// Public funtion which can be called by a UI button which will reverse the lerp of the camera hide the mouse again
    /// </summary>
    /// <param name="button"></param>
    public void ReturnButton(GameObject button) {
        Transform panel = button.transform.root;
        StartCoroutine(ActivatePanel(false, panel));
    }

    /// <summary>
    /// Starts the camera shake and activates the particle system which looks like a missile firing
    /// </summary>
    IEnumerator MissileLaunch() {
        cameraShakeScript.Shake();
        missile.SetActive(true);
        yield return new WaitForSeconds(2f);
        missile.SetActive(false);
    }
}
