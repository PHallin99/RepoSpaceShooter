using Components;
using Player;
using Systems;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthor : MonoBehaviour {
    public float rotationSpeed;
    public float thrustAmount;
    public float collisionRadius;
    public int playerStartLives;
    public GameObject laserPrefab;
}

public class PlayerBaker : Baker<PlayerAuthor> {
    public override void Bake(PlayerAuthor authoring) {
        var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
        // Add movement Component etc
        AddBuffer<PlayerDamageBufferElement>(playerEntity);
        AddComponent(playerEntity, new PlayerTag());
        AddComponent(playerEntity, new FireLaserTag());
        AddComponent(playerEntity, new ScreenWrapTag());

        AddComponent(playerEntity, new CircleCollisionRadius{
            Value = authoring.collisionRadius,
            Entity = playerEntity
        });
        AddComponent(playerEntity, new LaserPrefab{
            Value = GetEntity(authoring.laserPrefab, TransformUsageFlags.Dynamic)
        });
        AddComponent(playerEntity, new PlayerMoveProperties{
            RotationSpeed = authoring.rotationSpeed,
            ThrustAmount = authoring.thrustAmount
        });
        AddComponent(playerEntity, new PlayerMoveInput());
        AddComponent(playerEntity, new PlayerHealth{
            Value = authoring.playerStartLives
        });
    }
}