using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WarningMessage : MonoBehaviour {

    public TextMeshProUGUI warningMessage;

    public void display() {
        StartCoroutine(displaySelf());
    }

    IEnumerator displaySelf() {
        warningMessage.text = "Press escape first to play!";
        yield return new WaitForSeconds(2);
        warningMessage.text = "";
    }

}
