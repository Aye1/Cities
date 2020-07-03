using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovingObject))]
public class Character : MonoBehaviour
{
    public Building attachedBuilding;
    private MovingObject _moving;

    private int woodCarried = 0;

    // Start is called before the first frame update
    void Start()
    {
        _moving = GetComponent<MovingObject>();
        _moving.OnObjectReached += OnObjectReached;
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

        if(g.GetComponent<Tree>() != null)
        {
            OnTreeReached(g.GetComponent<Tree>());
        }
    }

    private void OnWaypointReached(Waypoint w)
    {
        if(w is BuildingWaypoint)
        {
            Building b = (w as BuildingWaypoint).building;
            if (attachedBuilding != b)
            {
                if (ShouldAttachToBuilding(b))
                {
                    attachedBuilding = b;
                    if (b is Sawmill)
                    {
                        _moving.sawingMode = true;
                        _moving.attachedBuilding = b;
                    }
                }
            }
            if(b is Sawmill)
            {
                UnloadCarriedWood(b as Sawmill);
            }
        }
    }

    private void OnTreeReached(Tree t)
    {
        t.CutWood(1);
        woodCarried++;
        Debug.Log("wood carried");
    }

    private void UnloadCarriedWood(Sawmill s)
    {
        s.AddWood(woodCarried);
        woodCarried = 0;
    }

    private bool ShouldAttachToBuilding(Building b)
    {
        return Alea.GetFloat(0.0f, 1.0f) <= 0.5f;
    }
    
}
