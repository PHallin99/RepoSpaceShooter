using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems {
    public partial struct FireLaserSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (laserPrefab, transform) in SystemAPI.Query<LaserPrefab, LocalTransform>().WithAll<FireLaserTag>()) {
                var newLaser = ecb.Instantiate(laserPrefab.Value);
                var laserTransform = LocalTransform.FromPositionRotationScale(transform.Position, transform.Rotation, 1f);
                
                ecb.SetComponent(newLaser, laserTransform);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {
        }
    }
}