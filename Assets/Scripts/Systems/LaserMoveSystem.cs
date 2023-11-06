using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems {
    public partial struct LaserMoveSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, moveRate) in SystemAPI.Query<RefRW<LocalTransform>, LaserMoveRate>()) {
                transform.ValueRW.Position += transform.ValueRO.Right() * moveRate.Value * deltaTime;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {
        }
    }
}