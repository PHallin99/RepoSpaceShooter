﻿using System.Collections;
using UI;
using UnityEngine;

namespace Player {
    public class SpaceShip : MonoBehaviour {
        [SerializeField] private AnimationClip playerRespawnClip;
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private Transform laserSpawnPoint;

        private float angularVelocity;
        private float horizontalInput;

        private bool isOnCooldown;
        private bool isSpawnProtected;

        private PlayerAnimation playerAnimation;
        private float respawnTime;
        private float thrustForce;
        private UIUpdater uIScoreUpdater;
        private float verticalInput;

        private void Start() {
            transform.position = Vector3.zero;
            respawnTime = playerRespawnClip.length;
            playerAnimation = FindObjectOfType<PlayerAnimation>();
            uIScoreUpdater = FindObjectOfType<UIUpdater>();
        }

        private void Update() {
            horizontalInput = -Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            angularVelocity = horizontalInput * ConstantsHandler.RotationSpeed;
            thrustForce = verticalInput * ConstantsHandler.ThrustAmount;

            transform.Rotate(Vector3.forward * (angularVelocity * Time.deltaTime));
            transform.Translate(Vector3.right * (thrustForce * Time.deltaTime));

            if (Input.GetKey(KeyCode.Space)) {
                Shoot();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (isSpawnProtected || collision.CompareTag(ConstantsHandler.LaserTag)) {
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

            Instantiate(laserPrefab, laserSpawnPoint.position, transform.rotation);
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
            yield return new WaitForSeconds(ConstantsHandler.ShootCooldown);
            isOnCooldown = false;
        }
    }
}