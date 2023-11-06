﻿using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Systems {
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct ResetInputSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (tag, entity) in SystemAPI.Query<FireLaserTag>().WithEntityAccess()) {
                ecb.SetComponentEnabled<FireLaserTag>(entity, false);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {
        }
    }
}