using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct KeyObjectPair
    {
        public KeyCode key;
        public GameObject prefab;
    }

    public KeyObjectPair[] keyObjectPairs;
    public Transform spawnPoint;

    
    void Update()
    {
        foreach (var pair in keyObjectPairs) {
            if (Input.GetKeyDown(pair.key)) {
                SpawnObject(pair.prefab);
            }
        }
    }

    void SpawnObject(GameObject prefab) {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}
