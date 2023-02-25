using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLineSegment {

    public Vector3 endVertex1;
    public Vector3 endVertex2;

    public Vector3 start;
    public Vector3 end;

    public TrackLineSegment() {
    }

    public void update(Vector3 start, Vector3 end) {
        this.start = start;
        this.end = end;

        float angle = Mathf.Atan2(end.z-start.z,end.x-start.x);

        this.endVertex1 = new Vector3(
            end.x + Track.halfWidth*Mathf.Sin(angle),
            0,
            end.z - Track.halfWidth*Mathf.Cos(angle)
        );

        this.endVertex2 = new Vector3(
            end.x - Track.halfWidth*Mathf.Sin(angle),
            0,
            end.z + Track.halfWidth*Mathf.Cos(angle)
        );
    }
}
