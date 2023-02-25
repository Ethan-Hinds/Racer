using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour {
    
    public TextMeshProUGUI speed;

    Car car;

    void Start() {

        GameManager.onPlayButtonPressed += playButtonPressed;

        car = GameObject.Find("Car").GetComponent<Car>();
    }

    void playButtonPressed() {
        speed.text = "0";
        speed.color = Color.white;
    }

    void Update() {
        if (GameManager.status == "race") {
            float carSpeed = car.localVelocity.magnitude;
            speed.text = carSpeed.ToString("F0");
            speed.color = Color.Lerp(Color.white, Color.red, carSpeed/70);
        }
    }
}
