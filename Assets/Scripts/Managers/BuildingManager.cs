using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum BuildChoice { House, Inn, Sawmill };

[Serializable]
public struct BuildBinding
{
    public BuildChoice buildType;
    public GameObject template;
}

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

#pragma warning disable 0649
    [Header("Editor Bindings")]
    [SerializeField] private List<BuildBinding> _templates;
#pragma warning restore 0649


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
        GameObject template = _templates.First(x => x.buildType == _currentBuildChoice).template;
        GameObject house = Instantiate(template, position, Quaternion.Euler(0, Alea.GetInt(0,360),0), transform);
        foreach(Waypoint w in house.GetComponentsInChildren<Waypoint>())
        {
            WaypointManager.Instance.AddWaypoint(w);
        }
    }
}
