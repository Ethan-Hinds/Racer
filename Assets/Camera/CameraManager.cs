using UnityEngine;

public class CameraManager : MonoBehaviour {

    Car car;

    // float animationSpeed = 2f;
    float animationSpeed = 0;

    void Start() {

        GameManager.onBackButtonPressed += backButtonPressed;

        transform.position = new Vector3(0, 200, 0);
        transform.LookAt(new Vector3(0, 0, 0));
        
        car = GameObject.Find("Car").GetComponent<Car>();
    }

    void backButtonPressed() {
        transform.position = new Vector3(0, 200, 0);
        transform.LookAt(new Vector3(0, 0, 0));
        animationSpeed = 0;
    }

    void FixedUpdate() {
        if (GameManager.status == "edit") {
            if (Input.GetKey(KeyCode.RightArrow)) {
                transform.Translate(Vector3.right*1);
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.Translate(Vector3.left*1);
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                transform.Translate(Vector3.up*1);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                transform.Translate(Vector3.down*1);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                if (transform.position.y > 10) {
                    transform.Translate(Vector3.forward*2);
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
                transform.Translate(Vector3.back*2);
            }
        } else {
            Vector3 offset = new Vector3(Mathf.Sin(car.angle), 0, Mathf.Cos(car.angle));
            offset.x *= -7;
            offset.y = 3;
            offset.z *= -7;

            if (GameManager.status == "animate") {

                Quaternion targetRotation = new Quaternion(0.14218f, 0.69264f, -0.14217f, 0.69269f);

                transform.position = Vector3.Lerp(transform.position, car.rb.position+offset, animationSpeed*Time.fixedDeltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, animationSpeed*Time.deltaTime);
                animationSpeed += 0.05f;

            } else if (GameManager.status == "race") {
                transform.position = car.rb.position + offset;  
                transform.LookAt(car.rb.position);

                if (car.localVelocity.z > 0) {
                    transform.Translate(-transform.forward*car.localVelocity.magnitude/10, Space.World);
                } else {
                    transform.Translate(transform.forward*Mathf.Min(car.localVelocity.magnitude, 20)/10, Space.World);
                }
            }
        }
    }
}
