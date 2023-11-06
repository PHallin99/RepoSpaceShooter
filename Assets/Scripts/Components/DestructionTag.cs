using Unity.Entities;

namespace Components {
    public struct DestructionTag : IComponentData {
        public Entity Entity;
    }
}