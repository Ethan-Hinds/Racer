using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WallManager : MonoBehaviour {

    public GameObject wallPrefab;
    public Transform ground;

    GameObject wall1;
    GameObject wall2;
    GameObject wall3;
    GameObject wall4;

    Track track;

    void Start() {
        GameManager.onBackButtonPressed += backButtonPressed;
        
        Ground.groundSizeSet += createWalls;
    }

    void backButtonPressed() {
        Destroy(wall1);
        Destroy(wall2);
        Destroy(wall3);
        Destroy(wall4);
    }

    void createWalls() {
        Vector3 center = ground.position;
        float xLength = ground.localScale.x;
        float zLength = ground.localScale.z;
        float height = 10;
        float thickness = 2;

        Vector3 location1 = center + new Vector3((xLength + thickness)/2, height/2, 0);
        Vector3 location2 = center + new Vector3(-(xLength + thickness)/2, height/2, 0);
        Vector3 location3 = center + new Vector3(0, height/2, (zLength + thickness)/2);
        Vector3 location4 = center + new Vector3(0, height/2, -(zLength + thickness)/2);

        wall1 = (GameObject) Instantiate(wallPrefab, location1, Quaternion.identity);
        wall1.transform.localScale = new Vector3(thickness, height, zLength);

        wall2 = (GameObject) Instantiate(wallPrefab, location2, Quaternion.identity);
        wall2.transform.localScale = new Vector3(thickness, height, zLength);

        wall3 = (GameObject) Instantiate(wallPrefab, location3, Quaternion.identity);
        wall3.transform.localScale = new Vector3(xLength, height, thickness);

        wall4 = (GameObject) Instantiate(wallPrefab, location4, Quaternion.identity);
        wall4.transform.localScale = new Vector3(xLength, height, thickness);
    }
}
