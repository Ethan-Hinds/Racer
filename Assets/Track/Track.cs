using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Track : MonoBehaviour {

    public static float halfWidth = 5;
    public static bool inEditMode = true;

    public Mesh mesh;

    public List<TrackSegment> segments = new List<TrackSegment>();

    List<Vector3> verticesList = new List<Vector3>();
    List<int> trianglesList = new List<int>();

    void Start() {
        startTrack();
    }

    void startTrack() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        TrackSegment segment = new TrackSegment();
        segments.Add(segment);

        verticesList.Add(new Vector3(0, 0, -halfWidth));
        verticesList.Add(new Vector3(0, 0, halfWidth));
        verticesList.Add(segment.lineSegments[0].endVertex1);
        verticesList.Add(segment.lineSegments[0].endVertex2);
        
        trianglesList.Add(verticesList.Count - 4);
        trianglesList.Add(verticesList.Count - 3);
        trianglesList.Add(verticesList.Count - 2);
        trianglesList.Add(verticesList.Count - 3);
        trianglesList.Add(verticesList.Count - 1);
        trianglesList.Add(verticesList.Count - 2);

        mesh.vertices = verticesList.ToArray();
        mesh.triangles = trianglesList.ToArray();

        TrackSegment lastSegment = segments[0];
        addSegment(lastSegment.lineSegments[lastSegment.lineSegments.Count-1]);
    }


    void addSegment(TrackLineSegment previousLineSegment) {

        TrackSegment segment = new TrackSegment(previousLineSegment);
        segments.Add(segment);

        foreach(TrackLineSegment lineSegment in segment.lineSegments) {

            verticesList.Add(lineSegment.endVertex1);
            verticesList.Add(lineSegment.endVertex2);

            trianglesList.Add(verticesList.Count - 4);
            trianglesList.Add(verticesList.Count - 3);
            trianglesList.Add(verticesList.Count - 2);
            trianglesList.Add(verticesList.Count - 3);
            trianglesList.Add(verticesList.Count - 1);
            trianglesList.Add(verticesList.Count - 2);
        }

        mesh.vertices = verticesList.ToArray();
        mesh.triangles = trianglesList.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    void updateLastSegment() {

        Vector3[] vertices = new Vector3[mesh.vertices.Length];
        vertices[0] = new Vector3(0, 0, -halfWidth);
        vertices[1] = new Vector3(0, 0, halfWidth);
        int i = 2;
        foreach (TrackSegment segment in segments) {
            foreach(TrackLineSegment lineSegment in segment.lineSegments) {
                vertices[i] = lineSegment.endVertex1;
                vertices[i+1] = lineSegment.endVertex2;
                i += 2;
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

    }

    void deleteLastSegment() {

        TrackSegment segment = segments[segments.Count-1];
        segments.RemoveAt(segments.Count-1);

        for (int i = 0; i < segment.lineSegments.Count; i++) {
            verticesList.RemoveAt(verticesList.Count-1);
            verticesList.RemoveAt(verticesList.Count-1);

            trianglesList.RemoveAt(trianglesList.Count-1);
            trianglesList.RemoveAt(trianglesList.Count-1);
            trianglesList.RemoveAt(trianglesList.Count-1);
            trianglesList.RemoveAt(trianglesList.Count-1);
            trianglesList.RemoveAt(trianglesList.Count-1);
            trianglesList.RemoveAt(trianglesList.Count-1);
        }

        mesh.triangles = trianglesList.ToArray();
        mesh.vertices = verticesList.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    void Update() {
        if (GameManager.status == "edit") {
            if (inEditMode) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo)) {
                    segments[segments.Count-1].update(hitInfo.point);
                }

                if (Input.GetMouseButtonDown(0)) {
                    TrackSegment lastSegment = segments[segments.Count-1]; 
                    addSegment(lastSegment.lineSegments[lastSegment.lineSegments.Count-1]);
                }

                if (Input.GetKeyDown(KeyCode.Z)) {
                    if (segments.Count > 2) {
                        deleteLastSegment();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (inEditMode) {
                    deleteLastSegment();
                } else {
                    TrackSegment lastSegment = segments[segments.Count-1];
                    addSegment(lastSegment.lineSegments[lastSegment.lineSegments.Count-1]);
                }
                inEditMode = !inEditMode;
            }
            updateLastSegment();
        }
    }
}
