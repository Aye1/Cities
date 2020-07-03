using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingObject : MonoBehaviour
{
    private Vector3 _destination;
    //private Waypoint _destinationWaypoint;
    //private Waypoint _latesteWaypointReached;
    private float _eps = 0.1f;
    private Rigidbody _rbd;
    private bool _destinationReached = false;
    private GameObject _latestObjectReached;
    private GameObject _destinationObject;

    /*public delegate void WaypointEvent(Waypoint w);
    public WaypointEvent OnWaypointReached;*/

    public delegate void ObjectReachedEvent(GameObject g);
    public ObjectReachedEvent OnObjectReached;

    // Temp variables, I hope
    public bool sawingMode = false;
    public Building attachedBuilding;

    private void Awake()
    {
        MouseManager.Instance.MouseClicked += OnMouseClicked;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rbd = GetComponent<Rigidbody>();
        _destination = transform.position;
        GoToClosestWaypoint();
    }

    private void OnDestroy()
    {
        MouseManager.Instance.MouseClicked -= OnMouseClicked;
    }

    // Update is called once per frame
    void Update()
    {
        if(MovementManager.Instance.automaticMode && _destinationReached)
        {
            if (sawingMode && attachedBuilding != null)
            {
                if(_latestObjectReached == null || _latestObjectReached.GetComponent<Waypoint>() != null)
                {
                    GoTo((attachedBuilding as Sawmill).GetClosestTree());
                } else
                {
                    GoTo(attachedBuilding.MainWaypoint);
                }
            }
            else
            {
                Waypoint latestWaypoint = _latestObjectReached.GetComponent<Waypoint>();
                if (_latestObjectReached != null && latestWaypoint != null)
                {
                    GoToRandomConnectedWaypoint(latestWaypoint);
                }
                else
                {
                    GoToClosestWaypoint();
                }
            }
        }
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        if(_destinationObject != null)
        {
            _destination = _destinationObject.transform.position;
        }
        Vector3 velocity = Vector3.zero;
        if (Vector3.Distance(VectorUtils.YToZero(transform.position), VectorUtils.YToZero(_destination)) > _eps)
        {
            velocity = (_destination - transform.position).normalized * MovementManager.Instance.speed;
            velocity.y = 0.0f;
            _destinationReached = false;
        } else
        {
            _destinationReached = true;
            _latestObjectReached = _destinationObject;
            OnObjectReached?.Invoke(_latestObjectReached);
            /*if(_destinationWaypoint != null)
            {
                _latesteWaypointReached = _destinationWaypoint;
                OnWaypointReached?.Invoke(_latesteWaypointReached);
            }*/
        }

        _rbd.velocity = velocity;
    }

    private void OnMouseClicked(Vector3 position)
    {
        if(!MovementManager.Instance.automaticMode)
            GoTo(new Vector2(position.x, position.z));
    }

    private void GoToClosestWaypoint()
    {
        GoTo(WaypointManager.Instance.GetClosestWaypoint(transform));
    }

    private void GoToRandomConnectedWaypoint(Waypoint currentWaypoint)
    {
        GoTo(WaypointManager.Instance.GetRandomConnectedWaypoint(currentWaypoint));
    }

    private void GoToRandomWaypoint()
    {
        GoTo(WaypointManager.Instance.GetRandomWaypoint());
    }

    public void GoTo(Waypoint waypoint)
    {
        GoTo(waypoint.gameObject);
        //_destinationWaypoint = waypoint;
        //destinationObject = null;
        // _destination will just be set in the Update()
    }

    public void GoTo(GameObject obj)
    {
        _destinationObject = obj;
    }

    public void GoTo(Vector3 position)
    {
        //_destinationWaypoint = null;
        _destinationObject = null;
        _destination = position;
    }

    // Go to position, but keep the current Y
    public void GoTo(Vector2 position)
    {
        GoTo(new Vector3(position.x, transform.position.y, position.y));
    }
}
