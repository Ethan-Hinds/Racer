using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopSpeed : MonoBehaviour {

    public TextMeshProUGUI speed;
    public TextMeshProUGUI topSpeed;

    int topCarSpeed = 0;

    void Start() {
        GameManager.onPlayButtonPressed += playButtonPressed;
        topSpeed.text = "Top Speed: 0";
    }

    void playButtonPressed() {
        topCarSpeed = 0;
        topSpeed.text = "Top Speed: 0";
    }

    void Update() {
        if (GameManager.status == "race") {
            int carSpeed = int.Parse(speed.text);
            if (carSpeed > topCarSpeed) {
                topSpeed.text = "Top Speed: " + carSpeed.ToString();
                topCarSpeed = carSpeed;
            }
        }
    }
}
