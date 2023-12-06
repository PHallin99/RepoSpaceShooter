using UI;
using UnityEngine;

namespace Player {
    public class Laser : MonoBehaviour, IPoolObject {
        [SerializeField] private float maxLifeTime;
        private LaserPool laserPool;
        private float lifeTimeSeconds;
        private float moveSpeed;
        private UIUpdater updater;

        private void Awake() {
            updater = FindObjectOfType<UIUpdater>();
            laserPool = FindObjectOfType<LaserPool>();
        }

        private void Update() {
            transform.Translate(Vector3.right * (Constants.LaserMovementSpeed * Time.deltaTime));

            if (lifeTimeSeconds >= maxLifeTime) {
                ResetObject();
                laserPool.ReturnObject(this);
                return;
            }

            lifeTimeSeconds += Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            updater.AddScore(-5);
            ResetObject();
            laserPool.ReturnObject(this);
        }

        public void EnableObject() {
            lifeTimeSeconds = 0f;
            gameObject.SetActive(true);
        }

        public void ResetObject() {
            lifeTimeSeconds = 0f;
            gameObject.SetActive(false);
        }
    }
}