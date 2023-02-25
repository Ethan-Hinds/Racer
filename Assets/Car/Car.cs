using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    public Vector3 localVelocity;
    public float angle;
    public Rigidbody rb;

    float maxTurnSpeed = 80;
    float carSpeed = 1000;

    Track track;
    List<TrackSegment> segments = new List<TrackSegment>();

    void Start() {
        GameManager.onPlayButtonPressed += playButtonPressed;
        GameManager.onBackButtonPressed += backButtonPressed;

        rb.isKinematic = true;
        track = GameObject.Find("Track").GetComponent<Track>();

        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 90, 0);
        angle = Mathf.PI/2;

    }

    void playButtonPressed() {
        rb.isKinematic = false;
    }

    void backButtonPressed() {
        rb.isKinematic = true;
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 90, 0);
        angle = Mathf.PI/2;
    }

    void FixedUpdate() {

        if (GameManager.status == "race") {

            if (Input.GetKey("left")) {
                turn(-1);
            }

            if (Input.GetKey("right")) {
                turn(1);
            }

            if (Input.GetKey("up")) {
                move(1);
            }

            if (Input.GetKey("down")) {
                move(-2);
            }

            float speed = rb.velocity.magnitude;
            localVelocity = transform.InverseTransformDirection(rb.velocity);
            angle = transform.rotation.eulerAngles.y*Mathf.Deg2Rad;
            float sign = Mathf.Sign(localVelocity.z);
            rb.velocity = new Vector3(
                speed*Mathf.Sin(angle)*sign,
                0,
                speed*Mathf.Cos(angle)*sign
            );
            if (track != null) {
                getDistanceFromTrack();
            }
        }
    }

    void turn(int dir) {
        float turnSpeed = dir*maxTurnSpeed*Mathf.Pow(localVelocity.magnitude, 0.5f)/4*Mathf.Sign(localVelocity.z);
        transform.Rotate(0, turnSpeed*Time.fixedDeltaTime, 0);
    }

    void move(int amt) {
        rb.AddRelativeForce(0, 0, amt*carSpeed*Time.fixedDeltaTime);
    }


    void trackComplete() {
        track = GameObject.Find("Track").GetComponent<Track>();
    }


    void getDistanceFromTrack() {
        float minDistanceSquared = Mathf.Infinity;
        foreach (TrackSegment segment in track.segments) {
            foreach (TrackLineSegment lineSegment in segment.lineSegments) {
                float distanceSquared = getDistToLineSquared(lineSegment.start, lineSegment.end, transform.position);
                minDistanceSquared = distanceSquared < minDistanceSquared ? distanceSquared : minDistanceSquared;
            }
        }

        if (minDistanceSquared > Mathf.Pow(Track.halfWidth, 2)) {
            rb.drag = 1.25f;
        } else {
            rb.drag = 0;
        }
    }

    public float getDistToLineSquared(Vector3 start, Vector3 end, Vector3 point) {
        float lengthSquared = (end - start).sqrMagnitude;
        float t = ((point.x - start.x)*(end.x - start.x) + (point.y - start.y)*(end.y - start.y) + (point.z - start.z)*(end.z-start.z))/lengthSquared;
        t = Mathf.Clamp(t, 0, 1);
        return (point - new Vector3(start.x+t*(end.x - start.x), start.y+t*(end.y-start.y), start.z+t*(end.z-start.z))).sqrMagnitude;
    }
}
