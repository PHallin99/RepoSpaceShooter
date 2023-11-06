using Player;
using Systems;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Components {
    public readonly partial struct PlayerAspect : IAspect {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> playerTransform;
        private readonly RefRW<PlayerHealth> playerHealth;
        private readonly RefRO<PlayerMoveInput> playerMoveInput;
        private readonly DynamicBuffer<PlayerDamageBufferElement> playerDamageBuffer;
        
        public void DamagePlayer() {
            if (playerDamageBuffer.Length > 0) {
                playerTransform.ValueRW.Position = float3.zero;
            }

            foreach (var playerDamageBufferElement in playerDamageBuffer) {
                playerHealth.ValueRW.Value -= playerDamageBufferElement.Value;
            }

            playerDamageBuffer.Clear();

            if (playerHealth.ValueRO.Value <= 0) {
                // GAME OVER
            }
        }
    }
}