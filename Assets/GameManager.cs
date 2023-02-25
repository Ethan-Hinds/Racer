using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static event System.Action onPlayButtonPressed;
    public static event System.Action onBackButtonPressed;

    public static string status;
    public GameObject ground;
    public GameObject car;
    public GameObject editorCanvas;
    public GameObject racingCanvas;

    WarningMessage warningMessage;

    void Start() {
        warningMessage = GameObject.Find("WarningMessage").GetComponent<WarningMessage>();
        status = "edit";
        racingCanvas.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

    public void PlayButtonPress() {
        if (Track.inEditMode) {
            warningMessage.display();
        } else {
            status = "animate";
            editorCanvas.SetActive(false);
            racingCanvas.SetActive(true);
            
            if (onPlayButtonPressed != null) {
                onPlayButtonPressed();
            }
        }
    }

    public void backButtonPressed() {
        status = "edit";
        editorCanvas.SetActive(true);
        racingCanvas.SetActive(false);
        if (onBackButtonPressed != null) {
            onBackButtonPressed();
        }
    }


}
