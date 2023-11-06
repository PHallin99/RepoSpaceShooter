
using Unity.Entities;

namespace Player {
    public struct PlayerDamageBufferElement : IBufferElementData {
        public int Value;
        // Damage application:
        // var currentPlayerDamage = new PlayerDamageBufferElement { Value = 1 }; Create buffer
        // EntityCommandBuffer*.ParallelWriter ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        // * ecb.CreateCommandBuffer(state.WorldUnmanaged).*AsParallelWriter()*;* Make ecb create the commandbuffer
        // Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        // ecb.AppendToBuffer(*sortKey*, playerEntity, currentPlayerDamage); Add buffer to elements on the players damage buffer
    }
}