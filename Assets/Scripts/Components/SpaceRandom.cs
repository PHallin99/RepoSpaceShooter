using Unity.Entities;
using Unity.Mathematics;

namespace Components {
    public struct SpaceRandom : IComponentData {
        public Random Value;
    }
}