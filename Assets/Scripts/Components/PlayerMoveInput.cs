using Unity.Entities;
using Unity.Mathematics;

namespace Components {
    public struct PlayerMoveInput : IComponentData {
        public float2 Value;
    }

    public struct PlayerMoveProperties : IComponentData {
        public float RotationSpeed;
        public float ThrustAmount;
    }

    public struct PlayerTag : IComponentData {
    }

    public struct FireLaserTag : IComponentData, IEnableableComponent {
    }

    public struct LaserPrefab : IComponentData {
        public Entity Value;
    }
}