using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Components {
    public readonly partial struct AsteroidMoveAspect : IAspect {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> asteroidTransform;
        private readonly RefRO<AsteroidMoveProperties> asteroidMoveProperties;
        
        private float3 MoveRate => asteroidMoveProperties.ValueRO.MoveRate;
        private float3 MoveDirection => asteroidMoveProperties.ValueRO.MoveDirection;

        public void Move(float deltaTime) {
            var newTransform = asteroidTransform.ValueRO.Translate(MoveDirection * MoveRate * deltaTime);
            asteroidTransform.ValueRW = newTransform;
        }
    }
}