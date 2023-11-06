using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems {
    [UpdateInGroup(typeof(LateSimulationSystemGroup), OrderLast = true)]
    public partial struct ScreenWrapSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            foreach (var (transform, screenWrapTag) in SystemAPI.Query<RefRW<LocalTransform>, ScreenWrapTag>()) {
                if (transform.ValueRO.Position.x > ConstantsHandler.ScreenRangeX || transform.ValueRO.Position.x < -ConstantsHandler.ScreenRangeX) {
                    transform.ValueRW.Position.x = -transform.ValueRO.Position.x;
                }

                if (transform.ValueRO.Position.y > ConstantsHandler.ScreenRangeY || transform.ValueRO.Position.y < -ConstantsHandler.ScreenRangeY) {
                    transform.ValueRW.Position.y = -transform.ValueRO.Position.y;
                }
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {
        }
    }
}