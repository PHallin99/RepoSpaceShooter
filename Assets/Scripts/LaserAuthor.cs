using Components;
using Unity.Entities;
using UnityEngine;

public class LaserMono : MonoBehaviour {
    public float MoveRate;
}

public class LaserBaker : Baker<LaserMono> {
    public override void Bake(LaserMono authoring) {
        var laser = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(laser, new LaserMoveRate{
            Value = authoring.MoveRate
        });
        AddComponent(laser, new ScreenWrapTag());
    }
}