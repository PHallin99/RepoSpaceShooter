using Unity.Entities;

namespace Components {
    public readonly partial struct LaserAspect : IAspect {
        public readonly Entity Entity;

        private readonly RefRW<LaserMoveRate> laserMoveRate;

        public float LaserLifeTimer
        {
            get => laserMoveRate.ValueRO.LifeTimer;
            set => laserMoveRate.ValueRW.LifeTimer = value;
        }

        public bool TimeToDestroy => LaserLifeTimer <= 0;
    }
}