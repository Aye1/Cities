using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Bindings")]
#pragma warning disable 0649
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _characterHolder;
#pragma warning restore 0649

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCharacter()
    {
        GameObject newCharacter = Instantiate(_characterPrefab, _spawnPosition.position, Quaternion.identity, _characterHolder);
    }
}
