using System.Collections;
using UI;
using UnityEngine;

namespace Player {
    public class SpaceShip : MonoBehaviour {
        [SerializeField] private AnimationClip playerRespawnClip;
        [SerializeField] private Transform laserSpawnPoint;

        private float verticalInput;
        private float horizontalInput;

        private float angularVelocity;
        private float respawnTime;
        private float thrustForce;
        private bool isOnCooldown;
        private bool isSpawnProtected;

        private LaserPool laserPool;
        private PlayerAnimation playerAnimation;
        private UIUpdater uIScoreUpdater;

        private void Start() {
            transform.position = Vector3.zero;
            respawnTime = playerRespawnClip.length;
            playerAnimation = FindObjectOfType<PlayerAnimation>();
            uIScoreUpdater = FindObjectOfType<UIUpdater>();
            laserPool = FindObjectOfType<LaserPool>();
        }

        private void Update() {
            horizontalInput = -Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            angularVelocity = horizontalInput * Constants.RotationSpeed;
            thrustForce = verticalInput * Constants.ThrustAmount;

            transform.Rotate(Vector3.forward * (angularVelocity * Time.deltaTime));
            transform.Translate(Vector3.right * (thrustForce * Time.deltaTime));

            if (Input.GetKey(KeyCode.Space)) {
                Shoot();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (isSpawnProtected || collision.CompareTag(Constants.LaserTag)) {
                return;
            }

            PlayerDied();
        }

        private void PlayerDied() {
            gameObject.transform.position = Vector3.zero;
            uIScoreUpdater.RemoveLife();

            // Start respawn animation
            isSpawnProtected = true;
            if (gameObject.activeSelf) {
                StartCoroutine(PlayerRespawn());
            }
        }

        private void Shoot() {
            if (isOnCooldown) {
                return;
            }

            var poolObject = laserPool.GetObject();
            if (poolObject is not Laser laser) {
                return;
            }

            laser.EnableObject();
            var laserTransform = laser.transform;
            laserTransform.rotation = transform.rotation;
            laserTransform.position = laserSpawnPoint.position;

            isOnCooldown = true;
            StartCoroutine(ShootCooldown());
        }

        private IEnumerator PlayerRespawn() {
            isSpawnProtected = true;
            playerAnimation.StartRespawnAnimation();
            yield return new WaitForSeconds(respawnTime);
            isSpawnProtected = false;
        }

        private IEnumerator ShootCooldown() {
            yield return new WaitForSeconds(Constants.ShootCooldown);
            isOnCooldown = false;
        }
    }
}