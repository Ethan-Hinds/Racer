using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegment {

    public Vector3 start;
    public Vector3 end;

    public float xCenter;
    public float zCenter;

    public float rotateAngle;

    int precision = 20;

    TrackLineSegment previousLineSegment;
    public List<TrackLineSegment> lineSegments = new List<TrackLineSegment>();

    public TrackSegment() {
        this.previousLineSegment = null;
        this.start = new Vector3(0, 0, 0);
        this.end = new Vector3(30, 0, 0);
        TrackLineSegment lineSegment = new TrackLineSegment();
        lineSegments.Add(lineSegment);
        lineSegment.update(this.start, this.end);

    }


    public TrackSegment(TrackLineSegment previousLineSegment) {
        this.previousLineSegment = previousLineSegment;
        this.start = previousLineSegment.end;
        this.end = start;
        for (int i = 0; i < precision; i++) {
            lineSegments.Add(new TrackLineSegment());
        }
    }

    public void update(Vector3 end) {
        this.end = end;

        float x0 = previousLineSegment.start.x;
        float z0 = previousLineSegment.start.z;

        float x1 = start.x;
        float z1 = start.z;

        float x2 = end.x;
        float z2 = end.z;

        float xm = (x1 + x2)/2;
        float zm = (z1 + z2)/2;

        float d = z1 == z0 ? 1E-6f : z1 - z0;

         xCenter = (((x1-x0)/(d))*x1 + ((x1-x2)/(z2-z1))*xm + z1 - zm) / ((x1-x2)/(z2-z1) + (x1-x0)/(d));
         zCenter = ((x1-x2)/(z2-z1))*(xCenter-xm)+zm;

        Vector3 center = new Vector3(xCenter, 0, zCenter);

        float radius = Mathf.Sqrt(Mathf.Pow(x1-xCenter, 2) + Mathf.Pow(z1-zCenter, 2));

        float startRadialAngle = Mathf.Atan2(z1-zCenter,x1-xCenter);

        Vector3 previousLineSegmentVector = previousLineSegment.end - previousLineSegment.start;
        Vector3 toEndVector = end - start;
        rotateAngle = 2*Vector3.SignedAngle(previousLineSegmentVector, toEndVector, Vector3.down)*Mathf.Deg2Rad;

        float dAngle = rotateAngle/precision;
        float segmentLength = radius*dAngle;

        for (int i = 0; i < precision; i++) {
            float radialAngle = startRadialAngle + dAngle*i;
            float xStart = xCenter + radius*Mathf.Cos(radialAngle);
            float zStart = zCenter + radius*Mathf.Sin(radialAngle);
            float xEnd = xCenter + radius*Mathf.Cos(radialAngle+dAngle);
            float zEnd = zCenter + radius*Mathf.Sin(radialAngle+dAngle);
            Vector3 segmentStart = new Vector3(xStart, 0, zStart);
            Vector3 segmentEnd = new Vector3(xEnd, 0, zEnd);

            lineSegments[i].update(segmentStart, segmentEnd);
        }
    }
}
