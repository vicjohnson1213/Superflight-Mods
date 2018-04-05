using SFMF;
using UnityEngine;

namespace Jetpack
{
    public class Jetpack : IMod
    {
        private void FixedUpdate()
        {
            var boost = Input.GetKey(KeyCode.JoystickButton5) || Input.GetKey(KeyCode.LeftShift);

            if (boost)
            {
                PlayerMovement.Singleton.currentSpeed += 10;
            }
        }
    }
}
