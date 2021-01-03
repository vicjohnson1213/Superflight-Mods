using SFMF;
using System;

namespace UnlimitedFlight
{
    public class UnlimitedFlight : IMod
    {

        private const string SettingsPath = @".\SFMF\ModSettings\UnlimitedFlight.csv";

        private float MinSpeed { get; set; }
        private float MaxSpeed { get; set; }

        private void Start()
        {
            var settings = File.ReadAllLines(SettingsPath);

            foreach (var line in settings)
            {
                var parts = line.Split(',');

                if (parts[0] == "Setting")
                {
                    if (parts[1] == "MinSpeed")
                        MinSpeed = float.Parse(parts[2]);
                    if (parts[1] == "MaxSpeed")
                        MaxSpeed = float.Parse(parts[2]);
                }
            }
        }

        public void Update()
        {
            PlayerMovement.Singleton.currentSpeed = Math.Max(MinSpeed, PlayerMovement.Singleton.currentSpeed);
            PlayerMovement.Singleton.forwardSpeedLimits.min = -1 * MaxSpeed;
            PlayerMovement.Singleton.forwardSpeedLimits.max = MaxSpeed;
        }
    }
}
