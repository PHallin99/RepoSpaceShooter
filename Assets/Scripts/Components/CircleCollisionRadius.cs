using Unity.Entities;

namespace Components {
    public struct CircleCollisionRadius : IComponentData {
        public Entity Entity;
        public float Value;
    }
}