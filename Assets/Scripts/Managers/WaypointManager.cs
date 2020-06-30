using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaypointManager : MonoBehaviour
{
    [Header("Editor Bindings")]
    [SerializeField] private Waypoint _waypointTemplate;
    [SerializeField] private WaypointPath _pathTemplate;

    [Header("Parameters")]
    public float maxPathLength = 2.0f;

    private List<Waypoint> _waypoints;
    private List<LocalWaypoint> _localWaypoints;
    private List<WaypointPath> _paths;

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
            _localWaypoints = new List<LocalWaypoint>();
            _paths = new List<WaypointPath>();
            RegisterChildrenWaypoints();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //GenerateRandomWaypoints(10);
        CreatePaths();
    }

    private void Update()
    {
        CheckPathsConsistency();
    }

    private void RegisterChildrenWaypoints()
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

    private void GenerateRandomWaypoints(int number)
    {
        for(int i = 0; i < number; i++)
        {
            Waypoint newWaypoint = Instantiate(_waypointTemplate, Vector3.zero, Quaternion.identity, transform);
            newWaypoint.transform.localPosition = new Vector3(Alea.GetFloat(-5.0f, 5.0f), 0.0f, Alea.GetFloat(-5.0f, 5.0f));
            _waypoints.Add(newWaypoint);
        }
    }

    private void CreatePaths()
    {
        Waypoint[] arrWaypoints = _waypoints.ToArray();
        for(int i = 0; i < arrWaypoints.Length; i++)
        {
            for (int j = i+1; j < arrWaypoints.Length; j++)
            {
                Waypoint w1 = arrWaypoints[i];
                Waypoint w2 = arrWaypoints[j];
                if (Vector3.Distance(w1.transform.position, w2.transform.position) < maxPathLength)
                {
                    CreatePath(w1, w2);
                }
            }
        }
    }

    private void CreateLocalPaths(LocalWaypoint w)
    {
        foreach(Waypoint w2 in w.LocalWaypoints)
        {
            WaypointPath path = CreatePath(w, w2);
            path.pathColor = Color.blue;
        }
    }

    private WaypointPath CreatePath(Waypoint w1, Waypoint w2)
    {
        WaypointPath newPath = Instantiate(_pathTemplate, Vector3.zero, Quaternion.identity, transform);
        newPath.waypoint1 = w1;
        newPath.waypoint2 = w2;
        ConnectWaypoints(w1, w2);
        _paths.Add(newPath);
        return newPath;
    }

    private void DestroyPath(WaypointPath path)
    {
        DisconnectWaypoints(path.waypoint1, path.waypoint2);
        _paths.Remove(path);
        Destroy(path.gameObject);
    }

    private void ConnectWaypoints(Waypoint w1, Waypoint w2)
    {
        w1.ConnectWaypoint(w2);
        w2.ConnectWaypoint(w1);
    }

    private void DisconnectWaypoints(Waypoint w1, Waypoint w2)
    {
        w1.DisconnectWaypoint(w2);
        w2.DisconnectWaypoint(w1);
    }

    private IEnumerable<WaypointPath> GetPathsWithWaypoint(Waypoint w)
    {
        return _paths.Where(x => x.waypoint1 == w || x.waypoint2 == w);
    }

    private void CheckPathsConsistency()
    {
        foreach(Waypoint w in _waypoints)
        {
            if(w.transform.hasChanged)
            {
                List<WaypointPath> pathsToDestroy = new List<WaypointPath>();
                foreach(WaypointPath p in GetPathsWithWaypoint(w))
                {
                    if(p.Length() > maxPathLength)
                    {
                        pathsToDestroy.Add(p);
                    }
                }
                pathsToDestroy.ForEach(p => DestroyPath(p));

                foreach(Waypoint w2 in GetWaypointsNotConnectedTo(w))
                {
                    if(Distance(w, w2) < maxPathLength)
                    {
                        CreatePath(w, w2);
                    }
                }
            }
        }
    }

    public void AddWaypoint(Waypoint w)
    {
        if(!_waypoints.Contains(w))
        {
            if (w is LocalWaypoint lw)
            {
                _localWaypoints.Add(lw);
                CreateLocalPaths(lw);
            }
            else
            {
                _waypoints.Add(w);
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

    public Waypoint GetRandomConnectedWaypoint(Waypoint w)
    {
        List<Waypoint> possibleWaypoints = new List<Waypoint>();
        possibleWaypoints.AddRange(w.ConnectedWaypoints);
        possibleWaypoints.AddRange(GetLocalWaypointsConnectedTo(w));

        if(possibleWaypoints.Count == 0)
        {
            return null;
        }
        int id = Alea.GetInt(0, possibleWaypoints.Count);
        return possibleWaypoints.ToArray()[id];
    }

    public IEnumerable<Waypoint> GetLocalWaypointsConnectedTo(Waypoint w)
    {
        return _localWaypoints.Where(x => x.LocalWaypoints.Contains(w));
    }

    public IEnumerable<Waypoint> GetWaypointsNotConnectedTo(Waypoint w)
    {
        return _waypoints.Where(x => !x.ConnectedWaypoints.Contains(w) && x != w);
    }

    public float Distance(Waypoint w1, Waypoint w2)
    {
        return Vector3.Distance(w1.transform.position, w2.transform.position);
    }
}
