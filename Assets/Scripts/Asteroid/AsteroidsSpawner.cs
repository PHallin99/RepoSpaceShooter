using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroid {
    public class AsteroidsSpawner : MonoBehaviour {
        [SerializeField] private GameObject[] asteroidPrefabs;
        [SerializeField] private float spawnTick;

        private readonly List<GameObject> spawnedAsteroids = new List<GameObject>();

        private float elapsedTime;
        private float spawnTimeModifier;

        private void Start() {
            SpawnAsteroid();
        }

        private void Update() {
            if (spawnTimeModifier >= ConstantsHandler.MaxSpawnTimeModifier) {
                return;
            }

            elapsedTime += Time.deltaTime;
            if (!(elapsedTime >= spawnTick)) {
                return;
            }

            spawnTimeModifier += 0.5f;
            elapsedTime = 0;
        }

        private void SpawnAsteroid() {
            var selectedAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
            var selectedSpawnPoint = new Vector2(Random.Range(-ConstantsHandler.ScreenRangeX, ConstantsHandler.ScreenRangeX),
                Random.Range(-ConstantsHandler.ScreenRangeY, ConstantsHandler.ScreenRangeY));

            Instantiate(selectedAsteroid, selectedSpawnPoint, selectedAsteroid.transform.rotation);
            StartCoroutine(AsteroidSpawnCounter());
        }

        private IEnumerator AsteroidSpawnCounter() {
            yield return new WaitForSeconds(ConstantsHandler.TimeToSpawn - spawnTimeModifier);
            SpawnAsteroid();
        }
    }
}