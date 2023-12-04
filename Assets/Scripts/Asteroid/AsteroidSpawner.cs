using System.Collections;
using Enums;
using UnityEngine;

namespace Asteroid {
    public class AsteroidSpawner : MonoBehaviour {
        [SerializeField] private float spawnTick;

        private AsteroidsPool asteroidsPool;
        private float elapsedTime;
        private float spawnTimeModifier;

        private void Awake() {
            asteroidsPool = FindObjectOfType<AsteroidsPool>();
        }

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
            StartCoroutine(AsteroidSpawnCounter());
            var asteroidType = (AsteroidType)Random.Range(0, 3);
            var asteroid = asteroidsPool.GetAsteroid(asteroidType);
            if (!asteroid) {
                return;
            }

            var spawnPoint = new Vector2(Random.Range(-6.66f, 6.66f), Random.Range(-5, 5));

            asteroid.transform.position = spawnPoint;
            asteroid.SetActive(true);
        }

        private IEnumerator AsteroidSpawnCounter() {
            yield return new WaitForSeconds(ConstantsHandler.TimeToSpawn - spawnTimeModifier);
            SpawnAsteroid();
        }
    }
}