using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {

    public static event System.Action onBackButtonPressed;

    public void onPress() {
        if (onBackButtonPressed != null) {
            onBackButtonPressed();
        }
    }

}
