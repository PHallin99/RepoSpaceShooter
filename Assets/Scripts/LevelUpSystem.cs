using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial struct LevelUpSystem : ISystem {
    [BurstCompile]
    public void OnCreate(ref SystemState state) {
            
    }

    [BurstCompile]
    public void OnUpdate(ref LevelComponent levelComponent) {
        // levelComponent.Level += 1f * Time.deltaTime;
        // Debug.Log($"Level: {levelComponent.Level}");
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) {

    }
}