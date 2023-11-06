using Unity.Entities;
using Unity.Mathematics;

namespace Components {
    public struct LaserSpawnPoint : IComponentData {
        public float3 Value;
    }
}