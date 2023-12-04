public static class ConstantsHandler {
    // PlayerPrefs
    public const string HighScoreKey = "HighScore";
    public const string LastGameKey = "LastGameScore";

    // Tags
    public const string AsteroidTag = "Asteroid";
    public const string LaserTag = "Laser";

    // UI
    public const string MajorAsteroidMovementSpeedString = "MajorAsteroidMovementSpeed";
    public const string MediumAsteroidMovementSpeedString = "MediumAsteroidMovementSpeed";
    public const string MinorAsteroidMovementSpeedString = "MinorAsteroidMovementSpeed";
    public const string ProtectionDurationString = "ProtectionDuration";
    public const string TimeToSpawnString = "TimeToSpawn";
    public const string LaserMovementSpeedString = "LaserMovementSpeed";
    public const string ShootCooldownString = "ShootCooldown";
    public const string ThrustAmountString = "ThrustAmount";
    public const string RotationSpeedString = "RotationSpeed";

    //SpaceShip
    public static float RotationSpeed = 150;
    public static float ThrustAmount = 5;

    // Asteroids
    public static float MajorAsteroidMovementSpeed = 1.5f;
    public static float MediumAsteroidMovementSpeed = 2f;
    public static float MinorAsteroidMovementSpeed = 2.5f;
    public static float ProtectedDuration = 0.75f;
    public static float TimeToSpawn = 5f;

    // Laser
    public static float LaserMovementSpeed = 6f;
    public static float ShootCooldown = 0.15f;
    public const float MaxSpawnTimeModifier = 4.5f;

    public static void InitializeValues(float majorAsteroidMovementSpeed, float mediumAsteroidMovementSpeed,
        float minorAsteroidMovementSpeed, float protectedDuration, float timeToSpawn, float laserMovementSpeed,
        float shootCooldown) {
        MajorAsteroidMovementSpeed = majorAsteroidMovementSpeed;
        MediumAsteroidMovementSpeed = mediumAsteroidMovementSpeed;
        MinorAsteroidMovementSpeed = minorAsteroidMovementSpeed;
        ProtectedDuration = protectedDuration;
        TimeToSpawn = timeToSpawn;
        LaserMovementSpeed = laserMovementSpeed;
        ShootCooldown = shootCooldown;
    }
}