using System;
using System.Collections;
using Enums;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroid {
    public class Asteroid : MonoBehaviour, IPoolObject {
        [SerializeField] internal AsteroidType asteroidType;

        private Collider2D asteroidCollider;
        private Vector2 movementDirection = Vector2.zero;
        private float movementSpeed;

        private AsteroidsPool asteroidsPool;
        private UIUpdater uIUpdater;

        private void Awake() {
            uIUpdater = FindObjectOfType<UIUpdater>();
            asteroidsPool = FindObjectOfType<AsteroidsPool>();
            asteroidCollider = GetComponent<Collider2D>();
            movementDirection = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f));
            switch (asteroidType) {
                case AsteroidType.Major:
                    movementSpeed = Constants.MajorAsteroidMovementSpeed;
                    break;
                case AsteroidType.Medium:
                    movementSpeed = Constants.MediumAsteroidMovementSpeed;
                    break;
                case AsteroidType.Minor:
                    movementSpeed = Constants.MinorAsteroidMovementSpeed;
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

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag(Constants.LaserTag)) {
                GiveScore();
            }

            ResetObject();
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
            yield return new WaitForSeconds(Constants.ProtectedDuration);
            asteroidCollider.enabled = true;
        }

        public void EnableObject() {
            gameObject.SetActive(true);
            StartCoroutine(SpawnProtectionCounter());
        }

        public void ResetObject() {
            asteroidCollider.enabled = false;
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
                    asteroidsPool.ReturnObject(this);
                    gameObject.SetActive(false);
                    return;
                default:
                    Debug.LogError("Default case in Asteroid Collision Event should not be able to happen", this);
                    return;
            }

            for (var i = 0; i < splitAmount; i++) {
                var poolObject = asteroidsPool.GetObjectOfType(targetAsteroid);
                if (poolObject is not Asteroid newAsteroid) {
                    continue;
                }

                newAsteroid.transform.position = asteroid.transform.position;
                newAsteroid.gameObject.SetActive(true);
            }

            asteroidsPool.ReturnObject(this);
            asteroid.SetActive(false);
        }
    }
}