using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroid {
    public class AsteroidsPool : MonoBehaviour, IPool<IPoolObject> {
        [SerializeField] private GameObject minorAsteroidPrefab;
        [SerializeField] private GameObject mediumAsteroidPrefab;
        [SerializeField] private GameObject majorAsteroidPrefab;
        [SerializeField] private int minorPoolSize;
        [SerializeField] private int mediumPoolSize;
        [SerializeField] private int majorPoolSize;
        
        private readonly Stack<IPoolObject> majorAsteroidPool = new Stack<IPoolObject>();
        private readonly Stack<IPoolObject> mediumAsteroidPool = new Stack<IPoolObject>();
        private readonly Stack<IPoolObject> minorAsteroidPool = new Stack<IPoolObject>();

        private void Awake() {
            for (var i = 0; i < minorPoolSize; i++) {
                var o = Instantiate(minorAsteroidPrefab, transform, true).GetComponent<Asteroid>();
                o.gameObject.SetActive(false);
                minorAsteroidPool.Push(o);
            }

            for (var i = 0; i < mediumPoolSize; i++) {
                var o = Instantiate(mediumAsteroidPrefab, transform, true).GetComponent<Asteroid>();
                o.gameObject.SetActive(false);
                mediumAsteroidPool.Push(o);
            }

            for (var i = 0; i < majorPoolSize; i++) {
                var o = Instantiate(majorAsteroidPrefab, transform, true).GetComponent<Asteroid>();
                o.gameObject.SetActive(false);
                majorAsteroidPool.Push(o);
            }
        }

        public IPoolObject GetObject() {
            var asteroidType = (AsteroidType)Random.Range(0, 2);
            switch (asteroidType) {
                case AsteroidType.Major:
                    return majorAsteroidPool.Count > 0 ? majorAsteroidPool.Pop() : null;
                case AsteroidType.Medium:
                    return mediumAsteroidPool.Count > 0 ? mediumAsteroidPool.Pop() : null;
                case AsteroidType.Minor:
                    return minorAsteroidPool.Count > 0 ? minorAsteroidPool.Pop() : null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ReturnObject(IPoolObject poolObject) {
            if (poolObject is not Asteroid asteroid) {
                return;
            }

            switch (asteroid.asteroidType) {
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
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IPoolObject GetObjectOfType(AsteroidType type) {
            switch (type) {
                case AsteroidType.Major:
                    return majorAsteroidPool.Count > 0 ? majorAsteroidPool.Pop() : null;
                case AsteroidType.Medium:
                    return mediumAsteroidPool.Count > 0 ? mediumAsteroidPool.Pop() : null;
                case AsteroidType.Minor:
                    return minorAsteroidPool.Count > 0 ? minorAsteroidPool.Pop() : null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}