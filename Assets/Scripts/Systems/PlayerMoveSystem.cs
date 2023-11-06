using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems {
    [UpdateInGroup(typeof(TransformSystemGroup))]
    public partial struct PlayerMoveSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;

            new SpaceShipMoveJob{
                DeltaTime = deltaTime
            }.Schedule();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {
        }
    }

    [BurstCompile]
    public partial struct SpaceShipMoveJob : IJobEntity {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ref LocalTransform transform, in PlayerMoveInput moveInput, PlayerMoveProperties moveProperties) {
            if (moveInput.Value.x != 0f) {
                var angularVelocity = -moveInput.Value.x * moveProperties.RotationSpeed;
                var deltaRotation = quaternion.RotateZ(angularVelocity * DeltaTime);
                transform.Rotation = math.mul(deltaRotation, transform.Rotation);
            }

            if (moveInput.Value.y != 0f) {
                var thrustForce = moveInput.Value.y * moveProperties.ThrustAmount;
                var newTransform = transform.Translate(transform.Right() * (thrustForce * DeltaTime));
                transform = newTransform;
            }
        }
    }
}