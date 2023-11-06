using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems {
    public partial struct CircleCollisionSystem : ISystem {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            state.Dependency.Complete();
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (collisionRadius, transform) in SystemAPI.Query<CircleCollisionRadius, LocalTransform>()) {
                foreach (var (collisionRadius2, transform2) in SystemAPI.Query<CircleCollisionRadius, LocalTransform>()) {
                    if (collisionRadius.Entity == collisionRadius2.Entity) {
                        continue;
                    }

                    var distance = math.distance(transform.Position, transform2.Position);
                    if (!(distance < collisionRadius.Value + collisionRadius2.Value)) {
                        continue;
                    }

                    if (state.EntityManager.HasComponent<PlayerTag>(collisionRadius.Entity)) {
                        if (!state.EntityManager.HasComponent<LaserMoveRate>(collisionRadius2.Entity)) {
                            ecb.SetComponent(collisionRadius.Entity, new LocalTransform{
                                Position = float3.zero,
                                Rotation = Quaternion.identity,
                                Scale = 1f
                            });
                        }
                    }

                    else {
                        ecb.DestroyEntity(collisionRadius.Entity);
                    }
                }
            }

            ecb.Playback(state.EntityManager);
        }
    }
}