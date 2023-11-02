using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class EntityTesting : MonoBehaviour {
    private void Start() {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var entityArchetype = entityManager.CreateArchetype(typeof(LevelComponent), typeof(LocalTransform));
        var entityArray = new NativeArray<Entity>(2000, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);
        foreach (var entity in entityArray) {
            entityManager.SetComponentData(entity, new LevelComponent{ Level = Random.Range(1, 20) });
        }

        entityArray.Dispose();
    }
}