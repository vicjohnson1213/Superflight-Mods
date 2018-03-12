using SFMF;
using System;

namespace UnlimitedFlight
{
    public class UnlimitedFlight : IMod
    {
        public void Update()
        {
            PlayerMovement.Singleton.currentSpeed = Math.Max(800, PlayerMovement.Singleton.currentSpeed);
        }
    }
}
