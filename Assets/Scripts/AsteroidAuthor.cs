using Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class AsteroidAuthor : MonoBehaviour {
    public uint randomSeed;
    public float collisionRadius;
}

public class AsteroidBaker : Baker<AsteroidAuthor> {
    public override void Bake(AsteroidAuthor authoring) {
        var asteroidEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(asteroidEntity, new ScreenWrapTag());
        AddComponent(asteroidEntity, new LocalTransform());
        AddComponent(asteroidEntity, new CircleCollisionRadius{
            Value = authoring.collisionRadius,
            Entity = asteroidEntity
        });
        AddComponent(asteroidEntity, new SpaceRandom{
            Value = Unity.Mathematics.Random.CreateFromIndex(authoring.randomSeed)
        });
    }
}