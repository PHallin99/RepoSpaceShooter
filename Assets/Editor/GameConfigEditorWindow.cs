using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class GameConfigEditorWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset;

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            var root = rootVisualElement;

            // VisualElements objects can contain other VisualElement following a tree hierarchy.
            VisualElement label = new Label("Asteroids Game Config Tool");
            root.Add(label);

            // Create a new field, disable it, and give it a style class.
            var majorAsteroidMovementSpeed = new Slider("Major Asteroid Movement Speed")
            {
                name = Constants.MajorAsteroidMovementSpeedString,
                value = Constants.MajorAsteroidMovementSpeed,
                showInputField = true
            };
            root.Add(majorAsteroidMovementSpeed);

            var mediumAsteroidMovementSpeed = new Slider("Medium Asteroid Movement Speed")
            {
                name = Constants.MediumAsteroidMovementSpeedString,
                value = Constants.MediumAsteroidMovementSpeed,
                showInputField = true
            };
            root.Add(mediumAsteroidMovementSpeed);

            var minorAsteroidMovementSpeed = new Slider("Minor Asteroid Movement Speed")
            {
                name = Constants.MinorAsteroidMovementSpeedString,
                value = Constants.MinorAsteroidMovementSpeed,
                showInputField = true
            };
            root.Add(minorAsteroidMovementSpeed);

            var protectionDuration = new Slider("Major Asteroid Movement Speed")
            {
                name = Constants.ProtectionDurationString,
                value = Constants.ProtectedDuration,
                showInputField = true
            };
            root.Add(protectionDuration);

            var timeToSpawn = new Slider("Asteroid Spawn Interval")
            {
                name = Constants.TimeToSpawnString,
                showInputField = true,
                value = Constants.TimeToSpawn,
                lowValue = 1
            };
            root.Add(timeToSpawn);

            var rotationSpeed = new Slider("Rotation Speed")
            {
                name = Constants.RotationSpeedString,
                lowValue = 1,
                highValue = 300,
                value = Constants.RotationSpeed,
                showInputField = true
            };
            root.Add(rotationSpeed);

            var thrustAmount = new Slider("Thrust Amount")
            {
                name = Constants.ThrustAmountString,
                value = Constants.ThrustAmount,
                showInputField = true
            };
            root.Add(thrustAmount);

            var laserMovementSpeed = new Slider("Laser Movement Speed")
            {
                name = Constants.LaserMovementSpeedString,
                value = Constants.LaserMovementSpeed,
                showInputField = true
            };
            root.Add(laserMovementSpeed);

            var shootCooldown = new Slider("Shooting Cooldown")
            {
                name = Constants.ShootCooldownString,
                value = Constants.ShootCooldown,
                showInputField = true
            };
            root.Add(shootCooldown);

            var button = new Button
            {
                name = "ApplyButton",
                text = "Apply in Play Mode"
            };

            root.Add(button);
            SetupButtonHandler();
        }

        [MenuItem("Window/UI Toolkit/GameConfig")]
        public static void ShowExample()
        {
            var wnd = GetWindow<GameConfigEditorWindow>();
            wnd.titleContent = new GUIContent("GameConfig");
        }

        // Functions as event handlers for your button click
        private void SetupButtonHandler()
        {
            var root = rootVisualElement;

            var buttons = root.Query<Button>();
            buttons.ForEach(RegisterHandler);
        }

        private void RegisterHandler(Button button)
        {
            button.RegisterCallback<ClickEvent>(ApplyValues);
        }

        private void ApplyValues(ClickEvent evt)
        {
            var root = rootVisualElement;

            Constants.MajorAsteroidMovementSpeed =
                root.Query<Slider>(Constants.MajorAsteroidMovementSpeedString).First().value;
            Constants.MediumAsteroidMovementSpeed =
                root.Query<Slider>(Constants.MediumAsteroidMovementSpeedString).First().value;
            Constants.MinorAsteroidMovementSpeed =
                root.Query<Slider>(Constants.MinorAsteroidMovementSpeedString).First().value;
            Constants.ProtectedDuration =
                root.Query<Slider>(Constants.ProtectionDurationString).First().value;
            Constants.TimeToSpawn = root.Query<Slider>(Constants.TimeToSpawnString).First().value;
            Constants.RotationSpeed = root.Query<Slider>(Constants.RotationSpeedString).First().value;
            Constants.ThrustAmount = root.Query<Slider>(Constants.ThrustAmountString).First().value;
            Constants.LaserMovementSpeed =
                root.Query<Slider>(Constants.LaserMovementSpeedString).First().value;
            Constants.ShootCooldown = root.Query<Slider>(Constants.ShootCooldownString).First().value;
        }
    }
}