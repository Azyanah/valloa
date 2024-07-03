using System;
using System.Collections;
using System.Collections.Generic;
using Animals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environement
{
    public class PlantSpawner : MonoBehaviour
    {
        public GameObject[] objectsToSpawn; // Array des objets à faire apparaître
        public int numberOfObjects = 10; // Nombre d'objets à faire apparaître
        private Renderer platformRenderer; // Renderer de la plateforme
        private Vector3 platformMin; // Coin minimum de la plateforme
        private Vector3 platformMax; // Coin maximum de la plateforme
        private List<GameObject> objects;
        public float spawnDelay = 0.5f; // Délai entre les apparitions
        public float growthDuration = 5.0f; // Durée de la croissance
        private float nextSpawnTime;

        void Start()
        {
            platformRenderer = GetComponent<Renderer>();
            platformMin = platformRenderer.bounds.min;
            platformMax = platformRenderer.bounds.max;
            objects = new List<GameObject>();
        }

        private void Update()
        {
            if (Time.time > nextSpawnTime) {
                SpawnObjects();
                nextSpawnTime = Time.time + spawnDelay;
            }
        }

        void SpawnObjects()
        {
            if (objects.Count < numberOfObjects)
            {
                // Déterminer une position aléatoire dans les limites de la plateforme
                Vector3 randomPosition = new Vector3(
                    Random.Range(platformMin.x, platformMax.x),
                    platformMax.y + 1, // Positionner le raycast légèrement au-dessus de la plateforme
                    Random.Range(platformMin.z, platformMax.z)
                );

                // Effectuer un raycast vers le bas pour trouver la surface de la plateforme
                if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit)) {
                    GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

                    GameObject spawnedObject = Instantiate(objectToSpawn, hit.point, Quaternion.identity);
                    // set random rotation
                    spawnedObject.transform.Rotate(Vector3.up, Random.Range(0, 360));
                    objects.Add(spawnedObject);
                
                    StartCoroutine(GrowObject(spawnedObject));
                }
            }
        }

        IEnumerator GrowObject(GameObject obj)
        {
            float elapsedTime = 0;
            Vector3 originalScale = Vector3.zero;
            Vector3 targetScale = obj.transform.localScale;
            obj.transform.localScale = originalScale;

            while (elapsedTime < growthDuration && obj != null) {
                obj.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / growthDuration);
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.localScale.y / 2, obj.transform.position.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            if (obj != null) {
                obj.transform.localScale = targetScale;
                if (obj.TryGetComponent(out Animals.AgentBehaviour agentBehaviour) && agentBehaviour.scalePlantInvincible <= 1) {
                    agentBehaviour.enabled = false;
                    if (agentBehaviour.CurrentState == AgentBehaviour.WanderState.Dead) {
                        Destroy(obj);
                        objects.Remove(obj);
                    }
                }
            }
        }
    }
}