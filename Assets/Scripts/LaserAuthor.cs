using Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserAuthor : MonoBehaviour {
    [FormerlySerializedAs("MoveRate")] public float moveRate;
    [FormerlySerializedAs("CollisionRadius")] public float collisionRadius;
    [FormerlySerializedAs("LifeTime")] public float lifeTime;
}

public class LaserBaker : Baker<LaserAuthor> {
    public override void Bake(LaserAuthor authoring) {
        var laser = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(laser, new CircleCollisionRadius{
            Value = authoring.collisionRadius, 
            Entity = laser
        });
        AddComponent(laser, new LaserMoveRate{
            Value = authoring.moveRate,
            LifeTimer = authoring.lifeTime
        });
        AddComponent(laser, new ScreenWrapTag());
    }
}