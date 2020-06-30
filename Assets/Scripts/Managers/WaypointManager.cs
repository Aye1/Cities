using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{

    private List<Waypoint> _waypoints;
    public WaypointPath templatePath;

    public IEnumerable<Waypoint> Waypoints
    {
        get { return _waypoints; }
    }

    public static WaypointManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _waypoints = new List<Waypoint>();
            GetChildrenWaypoints();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePaths();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetChildrenWaypoints()
    {
        foreach(Transform t in transform)
        {
            Waypoint waypoint = t.GetComponent<Waypoint>();
            if(waypoint != null)
            {
                _waypoints.Add(waypoint);
            }
        }
    }

    private void CreatePaths()
    {
        Waypoint[] arrWaypoints = _waypoints.ToArray();
        for(int i = 0; i < arrWaypoints.Length; i++)
        {
            for (int j = i+1; j < arrWaypoints.Length; j++)
            {
                WaypointPath newPath = Instantiate(templatePath, Vector3.zero, Quaternion.identity, transform);
                newPath.waypoint1 = arrWaypoints[i];
                newPath.waypoint2 = arrWaypoints[j];
            }
        }
    }

    public Waypoint GetClosestWaypoint(Vector3 position)
    {
        float minDist = float.MaxValue;
        Waypoint res = null;
        foreach(Waypoint w in _waypoints)
        {
            float dist = Vector3.Distance(position, w.transform.position);
            if(dist < minDist)
            {
                minDist = dist;
                res = w;
            }
        }
        return res;
    }

    public Waypoint GetClosestWaypoint(Transform t)
    {
        return GetClosestWaypoint(t.position);
    }

    public Waypoint GetRandomWaypoint()
    {
        int id = Alea.GetInt(0, _waypoints.Count);
        return _waypoints.ToArray()[id];
    }
}
