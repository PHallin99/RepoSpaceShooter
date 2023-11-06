using Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SpaceMono : MonoBehaviour {
    public int asteroidsToSpawn;
    public GameObject majorAsteroidPrefab;
    public GameObject mediumAsteroidPrefab;
    public GameObject minorAsteroidPrefab;
    public uint randomSeed;
    public float asteroidSpawnRate;
    public float asteroidMoveRate;
}

public class SpaceBaker : Baker<SpaceMono> {
    public override void Bake(SpaceMono authoring) {
        var spawnerEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(spawnerEntity, new SpaceProperties{
            MajorAsteroidPrefab = GetEntity(authoring.majorAsteroidPrefab, TransformUsageFlags.Dynamic),
            MediumAsteroidPrefab = GetEntity(authoring.mediumAsteroidPrefab, TransformUsageFlags.Dynamic),
            MinorAsteroidPrefab = GetEntity(authoring.minorAsteroidPrefab, TransformUsageFlags.Dynamic),
            ScreenDimensions = new float2(ConstantsHandler.ScreenRangeX, ConstantsHandler.ScreenRangeY),
            AsteroidsToSpawn = authoring.asteroidsToSpawn,
            AsteroidSpawnRate = authoring.asteroidSpawnRate,
            asteroidMoveRate = authoring.asteroidMoveRate
        });
        AddComponent(spawnerEntity, new SpaceRandom{
            Value = Random.CreateFromIndex(authoring.randomSeed)
        });
        AddComponent(spawnerEntity, new AsteroidSpawnTimer());
    }
}