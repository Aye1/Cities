using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalWaypoint : Waypoint
{
    [SerializeField] private List<Waypoint> _localWaypoints;

    public List<Waypoint> LocalWaypoints { get { return _localWaypoints; } }

    private void Start()
    {
        ConnectedWaypoints.AddRange(_localWaypoints);
    }
}
