using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovingObject))]
public class Character : MonoBehaviour
{
    public Building attachedBuilding;
    private MovingObject _moving;

    // Start is called before the first frame update
    void Start()
    {
        _moving = GetComponent<MovingObject>();
        _moving.OnWaypointReached += OnWaypointReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _moving.OnWaypointReached -= OnWaypointReached;
    }

    private void OnWaypointReached(Waypoint w)
    {
        if(w is BuildingWaypoint)
        {
            Building b = (w as BuildingWaypoint).building;
            if (ShouldAttachToBuilding(b))
            {
                attachedBuilding = b;
            }
        }
    }

    private bool ShouldAttachToBuilding(Building b)
    {
        return Alea.GetFloat(0.0f, 1.0f) <= 0.5f;
    }
}
