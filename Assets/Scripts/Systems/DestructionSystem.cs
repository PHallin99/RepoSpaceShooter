using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Systems {
    [UpdateAfter(typeof(LaserLifeTimeSystem))]
    [BurstCompile]
    public partial struct DestructionSystem : ISystem {
        public void OnUpdate(ref SystemState state) {
            state.Dependency.Complete();
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var destructionTag in SystemAPI.Query<DestructionTag>()) {
                ecb.DestroyEntity(destructionTag.Entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}