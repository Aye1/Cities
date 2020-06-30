using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Waypoint waypoint1;
    public Waypoint waypoint2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DrawPath();
    }

    public void DrawPath()
    {
        if (waypoint1 != null && waypoint2 != null)
        {
            Vector3 offset = Vector3.up * 0.1f;
            Debug.DrawLine(waypoint1.transform.position + offset, waypoint2.transform.position + offset, Color.red, 0.0f) ;
        }
    }

    public float Length()
    {
        return Vector3.Distance(waypoint1.transform.position, waypoint2.transform.position);
    }
}
