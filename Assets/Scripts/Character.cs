using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovingObject))]
public class Character : MonoBehaviour
{
    public Building attachedBuilding;
    private MovingObject _moving;

    private bool _sawingMode = false;

    // Start is called before the first frame update
    void Start()
    {
        _moving = GetComponent<MovingObject>();
        _moving.OnObjectReached += OnObjectReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _moving.OnObjectReached -= OnObjectReached;
    }

    private void OnObjectReached(GameObject g)
    {
        if(g.GetComponent<Waypoint>() != null)
        {
            OnWaypointReached(g.GetComponent<Waypoint>());
        }
    }

    private void OnWaypointReached(Waypoint w)
    {
        if(w is BuildingWaypoint)
        {
            Building b = (w as BuildingWaypoint).building;
            if (ShouldAttachToBuilding(b))
            {
                attachedBuilding = b;
                if(b is Sawmill)
                {
                    _moving.sawingMode = true;
                    _moving.attachedBuilding = b;
                }
            }
        }
    }

    private bool ShouldAttachToBuilding(Building b)
    {
        return Alea.GetFloat(0.0f, 1.0f) <= 0.5f;
    }

    private void GoToSawingModeTemp()
    {

    }
}
