using Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems {
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class GetPlayerInputSystem : SystemBase {
        private SpaceShipControlActions spaceShipControlActions;
        private Entity PlayerEntity;

        protected override void OnCreate() {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerMoveInput>();

            spaceShipControlActions = new SpaceShipControlActions();
        }

        protected override void OnStartRunning() {
            spaceShipControlActions.Enable();
            spaceShipControlActions.SpaceShipMap.PlayerShoot.performed += OnPlayerShoot;
            PlayerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        }

        protected override void OnUpdate() {
            var currentMoveInput = spaceShipControlActions.SpaceShipMap.PlayerMovement.ReadValue<Vector2>();
            SystemAPI.SetSingleton(new PlayerMoveInput{ Value = currentMoveInput });
        }

        protected override void OnStopRunning() {
            spaceShipControlActions.SpaceShipMap.PlayerShoot.performed -= OnPlayerShoot;
            spaceShipControlActions.Disable();
            PlayerEntity = Entity.Null;
        }

        private void OnPlayerShoot(InputAction.CallbackContext obj) {
            if (!SystemAPI.Exists(PlayerEntity)) {
                return;
            }

            SystemAPI.SetComponentEnabled<FireLaserTag>(PlayerEntity, true);
        }
    }
}