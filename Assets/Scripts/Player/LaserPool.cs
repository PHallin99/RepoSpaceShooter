using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class LaserPool : MonoBehaviour, IPool<IPoolObject> {
        [SerializeField] private int poolSize;
        [SerializeField] private GameObject laserPrefab;

        private readonly Stack<IPoolObject> laserPool = new Stack<IPoolObject>();

        private void Awake() {
            for (var i = 0; i < poolSize; i++) {
                var o = Instantiate(laserPrefab, transform).GetComponent<Laser>();
                o.gameObject.SetActive(false);
                laserPool.Push(o);
            }
        }

        public IPoolObject GetObject() {
            return laserPool.Count > 0 ? laserPool.Pop() : null;
        }

        public void ReturnObject(IPoolObject poolObject) {
            if (poolObject is Laser) {
                laserPool.Push(poolObject);
            }
        }
    }
}