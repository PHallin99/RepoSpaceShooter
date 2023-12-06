using System.Collections;
using UnityEngine;

namespace Asteroid {
    public class AsteroidSpawner : MonoBehaviour {
        [SerializeField] private float spawnTick;

        private IPool<IPoolObject> asteroidsPool;
        private float elapsedTime;
        private float spawnTimeModifier;

        private void Awake() {
            asteroidsPool = FindObjectOfType<AsteroidsPool>();
        }

        private void Start() {
            SpawnAsteroid();
        }

        private void Update() {
            if (spawnTimeModifier >= Constants.MaxSpawnTimeModifier) {
                return;
            }

            elapsedTime += Time.deltaTime;
            if (!(elapsedTime >= spawnTick)) {
                return;
            }

            // Ramp up spawning freq
            spawnTimeModifier += 0.5f;
            elapsedTime = 0;
        }

        private void SpawnAsteroid() {
            StartCoroutine(AsteroidSpawnCounter());
            var poolObject = asteroidsPool.GetObject();
            if (poolObject is not Asteroid asteroid) {
                return;
            }

            var spawnPoint = new Vector2(Random.Range(-6.66f, 6.66f), Random.Range(-5, 5));

            asteroid.transform.position = spawnPoint;
            asteroid.EnableObject();
        }

        private IEnumerator AsteroidSpawnCounter() {
            yield return new WaitForSeconds(Constants.TimeToSpawn - spawnTimeModifier);
            SpawnAsteroid();
        }
    }
}