using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingWaypoint MainWaypoint
    {
        get
        {
            BuildingWaypoint res = GetComponentInChildren<BuildingWaypoint>();
            return res;
        }
    }

    private void Awake()
    {
        RegisterInternalWaypoints();
        OnCreate();
    }

    protected virtual void OnCreate() { }

    private void RegisterInternalWaypoints()
    {
        foreach(BuildingWaypoint w in GetComponentsInChildren<BuildingWaypoint>())
        {
            w.building = this;
        }
    }
}
