using Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class AsteroidMono : MonoBehaviour {
    public uint randomSeed;
}

public class AsteroidBaker : Baker<AsteroidMono> {
    public override void Bake(AsteroidMono authoring) {
        var asteroidEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(asteroidEntity, new ScreenWrapTag());
        AddComponent(asteroidEntity, new LocalTransform());
        AddComponent(asteroidEntity, new SpaceRandom{
            Value = Unity.Mathematics.Random.CreateFromIndex(authoring.randomSeed)
        });
    }
}