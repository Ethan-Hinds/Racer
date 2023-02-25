using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Ground : MonoBehaviour {

    public static event System.Action groundSizeSet;

    Track track;

    void Start() {
        GameManager.onPlayButtonPressed += playButtonPressed;

        transform.position = new Vector3(0, -0.51f, 0);
        transform.localScale = new Vector3(10, 1, 10);
    }

    void Update() {
        if (GameManager.status == "edit") {
            transform.position = new Vector3(Camera.main.transform.position.x, -0.51f, Camera.main.transform.position.z);
            float size = Camera.main.transform.position.y * Mathf.Sin(Camera.main.fieldOfView * Mathf.Deg2Rad) * Camera.main.aspect * Camera.main.orthographicSize;
            transform.localScale = new Vector3(size, 1, size);
        }
    }

    void playButtonPressed() {
        setGroundSize();
    }

    void setGroundSize() {
        track = GameObject.Find("Track").GetComponent<Track>();

        float minX = track.mesh.vertices.Min(point => point.x);
        float minZ = track.mesh.vertices.Min(point => point.z);
        float maxX = track.mesh.vertices.Max(point => point.x);
        float maxZ = track.mesh.vertices.Max(point => point.z);

        Vector3 center = new Vector3(
            (maxX + minX)/2,
            -0.501f,
            (maxZ + minZ)/2   
        );

        Vector3 size = new Vector3(
            maxX - minX + 10,
            1,
            maxZ - minZ + 10
        );

        transform.position = center;
        transform.localScale = size;
        groundSizeSet();
    }
}
