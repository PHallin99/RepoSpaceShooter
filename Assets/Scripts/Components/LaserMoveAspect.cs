using Unity.Entities;
using Unity.Transforms;

namespace Components {
    public readonly partial struct LaserMoveAspect : IAspect {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> laserTransform;

        public void ScreenWrap() {
            if (laserTransform.ValueRO.Position.x > ConstantsHandler.ScreenRangeX || laserTransform.ValueRO.Position.x < -ConstantsHandler.ScreenRangeX) {
                laserTransform.ValueRW.Position.x = -laserTransform.ValueRO.Position.x;
            }

            if (laserTransform.ValueRO.Position.y > ConstantsHandler.ScreenRangeY || laserTransform.ValueRO.Position.y < -ConstantsHandler.ScreenRangeY) {
                laserTransform.ValueRW.Position.y = -laserTransform.ValueRO.Position.y;
            }

        }
    }
}