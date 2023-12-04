using System;
using System.Collections;
using Enums;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

// To clarify between System.Random and UnityEngine.Random

namespace Asteroid {
    public class Asteroid : MonoBehaviour {
        [SerializeField] private AsteroidType asteroidType;

        private Collider2D asteroidCollider;
        private AsteroidsPool asteroidsPool;
        private Vector2 movementDirection = Vector2.zero;
        private float movementSpeed;
        private UIUpdater uIUpdater;

        private void Awake() {
            uIUpdater = FindObjectOfType<UIUpdater>();
            asteroidsPool = FindObjectOfType<AsteroidsPool>();
            asteroidCollider = GetComponent<Collider2D>();
            movementDirection = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f));
            switch (asteroidType) {
                case AsteroidType.Major:
                    movementSpeed = ConstantsHandler.MajorAsteroidMovementSpeed;
                    break;
                case AsteroidType.Medium:
                    movementSpeed = ConstantsHandler.MediumAsteroidMovementSpeed;
                    break;
                case AsteroidType.Minor:
                    movementSpeed = ConstantsHandler.MinorAsteroidMovementSpeed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Start() {
            StartCoroutine(SpawnProtectionCounter());
        }

        private void Update() {
            transform.Translate(movementDirection.normalized * (movementSpeed * Time.deltaTime), Space.World);
        }

        private void OnEnable() {
            StartCoroutine(SpawnProtectionCounter());
        }

        private void OnDisable() {
            asteroidCollider.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            // if (isSpawnProtected) {
            //     return;
            // }

            if (collision.CompareTag(ConstantsHandler.LaserTag)) {
                GiveScore();
            }

            ManageCollision();
        }

        public void Respawn() {
            asteroidCollider.enabled = false;
            // isSpawnProtected = true;
            StartCoroutine(SpawnProtectionCounter());
        }

        private void ManageCollision() {
            var asteroid = gameObject;
            AsteroidType targetAsteroid;
            const int splitAmount = 3;

            switch (asteroidType) {
                case AsteroidType.Major:
                    targetAsteroid = AsteroidType.Medium;
                    break;
                case AsteroidType.Medium:
                    targetAsteroid = AsteroidType.Minor;
                    break;
                case AsteroidType.Minor:
                    asteroidsPool.ReturnAsteroid(asteroidType, asteroid);
                    gameObject.SetActive(false);
                    return;
                default:
                    Debug.LogError("Default case in DestroyAsteroid should not be able to happen", this);
                    return;
            }

            for (var i = 0; i < splitAmount; i++) {
                var splitAsteroid = asteroidsPool.GetAsteroid(targetAsteroid);
                if (!splitAsteroid) {
                    continue;
                }

                splitAsteroid.transform.position = asteroid.transform.position;
                splitAsteroid.SetActive(true);
            }

            asteroid.SetActive(false);
            asteroidsPool.ReturnAsteroid(asteroidType, asteroid);
        }

        private void GiveScore() {
            switch (asteroidType) {
                case AsteroidType.Major:
                    uIUpdater.AddScore(20);
                    break;
                case AsteroidType.Medium:
                    uIUpdater.AddScore(50);
                    break;
                case AsteroidType.Minor:
                    uIUpdater.AddScore(100);
                    break;
                default:
                    Debug.LogError("Default state when awarding score should not be able to happen", this);
                    break;
            }
        }

        private IEnumerator SpawnProtectionCounter() {
            yield return new WaitForSeconds(ConstantsHandler.ProtectedDuration);
            asteroidCollider.enabled = true;
            // isSpawnProtected = false;
        }
    }
}