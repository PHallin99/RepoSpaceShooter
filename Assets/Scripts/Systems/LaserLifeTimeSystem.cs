using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems {
    public partial struct LaserLifeTimeSystem : ISystem {
        public void OnCreate(ref SystemState state) {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            foreach (var (transform, moveRate) in SystemAPI.Query<RefRW<LocalTransform>, LaserMoveRate>()) {
                transform.ValueRW.Position += transform.ValueRO.Right() * moveRate.Value * deltaTime;
            }

            new LaserLifeTimeJob{
                DeltaTime = deltaTime,
                entityCommandBuffer = ecb.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule();
        }
    }

    [BurstCompile]
    public partial struct LaserLifeTimeJob : IJobEntity {
        public float DeltaTime;
        public EntityCommandBuffer entityCommandBuffer;

        [BurstCompile]
        private void Execute(LaserAspect laser, [ChunkIndexInQuery] int index) {
            laser.LaserLifeTimer -= DeltaTime;
            if (!laser.TimeToDestroy) {
                return;
            }

            entityCommandBuffer.AddComponent(laser.Entity, new DestructionTag{
                Entity = laser.Entity
            });
        }
    }
}