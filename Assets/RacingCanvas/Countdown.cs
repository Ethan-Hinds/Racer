using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour {

    public TextMeshProUGUI countdown;

    void Start() {
        GameManager.onPlayButtonPressed += playButtonPressed;
        StartCoroutine(startCountdown());
    }

    void playButtonPressed() {
        StartCoroutine(startCountdown());
    }

   IEnumerator startCountdown() {
       countdown.text = "";
        yield return new WaitForSeconds(2); 
        string[] msgs = new string[] {"3", "2", "1", "Go!", ""};
        foreach (string msg in msgs) {
            countdown.text = msg;
            if (msg == "Go!") {
                GameManager.status = "race";
            }
            yield return new WaitForSeconds(1);
        }
    }
}
