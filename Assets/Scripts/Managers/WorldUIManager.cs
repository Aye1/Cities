using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldUIManager : MonoBehaviour
{

    [Header("Editor bindings")]
    [SerializeField] private GameObject _woodGatherTemplate;

    public static WorldUIManager Instance { get; private set; }

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
    }

    public void DisplayWoodGatherUI(Vector3 position, int amount)
    {
        Vector3 noise = Alea.GetFloat(0.2f, 1.0f) * Vector3.up;
        GameObject obj = Instantiate(_woodGatherTemplate, position + noise, Quaternion.identity, transform);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "+ " + amount + " wood";
    }
}
