using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Asteroid {
    public class AsteroidsPool : MonoBehaviour {
        [SerializeField] private GameObject minorAsteroidPrefab;
        [SerializeField] private GameObject mediumAsteroidPrefab;
        [SerializeField] private GameObject majorAsteroidPrefab;
        [SerializeField] private int minorPoolSize;
        [SerializeField] private int mediumPoolSize;
        [SerializeField] private int majorPoolSize;

        private readonly Stack<GameObject> minorAsteroidPool = new Stack<GameObject>();
        private readonly Stack<GameObject> mediumAsteroidPool = new Stack<GameObject>();
        private readonly Stack<GameObject> majorAsteroidPool = new Stack<GameObject>();

        private void Awake() {
            for (var i = 0; i < minorPoolSize; i++) {
                var o = Instantiate(minorAsteroidPrefab, gameObject.transform, true);
                o.SetActive(false);
                minorAsteroidPool.Push(o);
            }

            for (var i = 0; i < mediumPoolSize; i++) {
                var o = Instantiate(mediumAsteroidPrefab, gameObject.transform, true);
                o.SetActive(false);
                mediumAsteroidPool.Push(o);
            }

            for (var i = 0; i < majorPoolSize; i++) {
                var o = Instantiate(majorAsteroidPrefab, gameObject.transform, true);
                o.SetActive(false);
                majorAsteroidPool.Push(o);
            }
        }

        public GameObject GetAsteroid(AsteroidType asteroidType) {
            switch (asteroidType) {
                case AsteroidType.Major:
                    return majorAsteroidPool.Count > 0 ? majorAsteroidPool.Pop() : null;
                case AsteroidType.Medium:
                    return mediumAsteroidPool.Count > 0 ? mediumAsteroidPool.Pop() : null;
                case AsteroidType.Minor:
                    return minorAsteroidPool.Count > 0 ? minorAsteroidPool.Pop() : null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(asteroidType), asteroidType, null);
            }
        }

        public void ReturnAsteroid(AsteroidType asteroidType, GameObject asteroid) {
            switch (asteroidType) {
                case AsteroidType.Major:
                    majorAsteroidPool.Push(asteroid);
                    break;
                case AsteroidType.Medium:
                    mediumAsteroidPool.Push(asteroid);
                    break;
                case AsteroidType.Minor:
                    minorAsteroidPool.Push(asteroid);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(asteroidType), asteroidType, null);
            }
        }
    }
}