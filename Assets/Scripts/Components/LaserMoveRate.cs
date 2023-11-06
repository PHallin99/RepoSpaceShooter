using Unity.Entities;

namespace Components {
    public struct LaserMoveRate : IComponentData {
        public float Value;
        public float LifeTimer;
    }
}