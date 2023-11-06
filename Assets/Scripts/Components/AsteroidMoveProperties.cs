using Unity.Entities;
using Unity.Mathematics;

namespace Components {
    public struct AsteroidMoveProperties : IComponentData {
        public float MoveRate;
        public float3 MoveDirection;
    }
}