using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingObject : MonoBehaviour
{
    private Vector3 _destination;
    private Waypoint _destinationWaypoint;
    private Waypoint _latesteWaypointReached;
    private float _eps = 0.1f;
    private Rigidbody _rbd;
    private bool _destinationReached = false;

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
            if(_latesteWaypointReached != null)
            {
                GoToRandomConnectedWaypoint(_latesteWaypointReached);
            } else
            {
                GoToClosestWaypoint();
            }
        }
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        if(_destinationWaypoint != null)
        {
            _destination = _destinationWaypoint.transform.position;
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
            if(_destinationWaypoint != null)
            {
                _latesteWaypointReached = _destinationWaypoint;
            }
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
        _destinationWaypoint = waypoint;
    }

    public void GoTo(Vector3 position)
    {
        _destinationWaypoint = null;
        _destination = position;
    }

    // Go to position, but keep the current Y
    public void GoTo(Vector2 position)
    {
        GoTo(new Vector3(position.x, transform.position.y, position.y));
    }
}
