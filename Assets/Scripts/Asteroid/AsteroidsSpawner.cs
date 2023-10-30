﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid {
    public class AsteroidsSpawner : MonoBehaviour {
        [SerializeField] private GameObject[] asteroidPrefabs;

        private readonly List<GameObject> spawnedAsteroids = new List<GameObject>();

        private void Start() {
            SpawnAsteroid();
        }

        private void SpawnAsteroid() {
            var selectedAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
            var selectedSpawnPoint = new Vector2(Random.Range(-6.66f, 6.66f), Random.Range(-5, 5));

            Instantiate(selectedAsteroid, selectedSpawnPoint, selectedAsteroid.transform.rotation);
            StartCoroutine(AsteroidSpawnCounter());
        }

        private IEnumerator AsteroidSpawnCounter() {
            yield return new WaitForSeconds(ConstantsHandler.TimeToSpawn);
            SpawnAsteroid();
        }
    }
}