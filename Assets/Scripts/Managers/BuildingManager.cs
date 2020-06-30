using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager Instance { get; private set; }

    [Header("Editor Bindings")]
    [SerializeField] private GameObject _houseTemplate;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        MouseManager.Instance.MouseClicked += ManageClick;
    }

    private void OnDestroy()
    {
        MouseManager.Instance.MouseClicked -= ManageClick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ManageClick(Vector3 clickPos)
    {
        CreateHouse(clickPos);
    }

    private void CreateHouse(Vector3 position)
    {
        GameObject house = Instantiate(_houseTemplate, position, Quaternion.identity, transform);
        Waypoint w = house.GetComponentInChildren<Waypoint>();
        if(w != null)
        {
            WaypointManager.Instance.AddWaypoint(w);
        }
    }
}
