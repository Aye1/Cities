using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BuildChoice { House, Inn };

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [Header("Editor Bindings")]
    [SerializeField] private GameObject _houseTemplate;
    [SerializeField] private GameObject _innTemplate;

    private BuildChoice _currentBuildChoice;

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
        ManageKeyboard();
    }

    private void ManageKeyboard()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            _currentBuildChoice = (BuildChoice)(((int)_currentBuildChoice + 1) % Enum.GetNames(typeof(BuildChoice)).Length);
        }
    }
    
    private void ManageClick(Vector3 clickPos)
    {
        CreateHouse(clickPos);
    }

    private void CreateHouse(Vector3 position)
    {
        GameObject template = _currentBuildChoice == BuildChoice.House ? _houseTemplate : _innTemplate;
        GameObject house = Instantiate(template, position, Quaternion.identity, transform);
        foreach(Waypoint w in house.GetComponentsInChildren<Waypoint>())
        {
            WaypointManager.Instance.AddWaypoint(w);
        }
    }
}
