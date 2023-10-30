using UI;
using UnityEngine;

namespace Player {
    public class Laser : MonoBehaviour {
        [SerializeField] private float maxLifeTime;
        private float moveSpeed;
        private float lifeTimeSeconds;

        private void Update() {
            transform.Translate(Vector3.right * (ConstantsHandler.LaserMovementSpeed * Time.deltaTime));

            if (lifeTimeSeconds >= maxLifeTime) {
                Destroy(gameObject);
                return;
            }

            lifeTimeSeconds += Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            FindObjectOfType<UIUpdater>().AddScore(-5);
            Destroy(gameObject);
        }
    }
}