using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private List<Waypoint> _connectedWaypoints;

    public List<Waypoint> ConnectedWaypoints { get { return _connectedWaypoints; } }

    private void Awake()
    {
        _connectedWaypoints = new List<Waypoint>();
    }

    public void ConnectWaypoint(Waypoint w)
    {
        if(!_connectedWaypoints.Contains(w))
        {
            _connectedWaypoints.Add(w);
        }
    }

    public void DisconnectWaypoint(Waypoint w)
    {
        _connectedWaypoints.Remove(w);
    }
}
