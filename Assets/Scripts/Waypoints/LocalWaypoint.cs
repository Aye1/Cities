using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalWaypoint : Waypoint
{

#pragma warning disable 0649
    [Header("Editor bindings")]
    [SerializeField] private List<Waypoint> _localWaypoints;
#pragma warning restore 0649

    public List<Waypoint> LocalWaypoints { get { return _localWaypoints; } }

    private void Start()
    {
        ConnectedWaypoints.AddRange(_localWaypoints);
    }
}
