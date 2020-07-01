using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    private void Awake()
    {
        RegisterInternalWaypoints();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RegisterInternalWaypoints()
    {
        foreach(BuildingWaypoint w in GetComponentsInChildren<BuildingWaypoint>())
        {
            w.building = this;
        }
    }
}
