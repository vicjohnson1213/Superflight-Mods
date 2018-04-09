using SFMF;
using UnityEngine;

namespace Jetpack
{
    public class Jetpack : IMod
    {
        private float acceleration;

        private KeyCode KeyboardKey;
        private KeyCode ControllerButton;

        private void Start()
        {
            var settings = Util.ReadModSettings("Jetpack");

            var accelerationControl = settings.GetControl("boost");
            KeyboardKey = accelerationControl?.KeyboardKey ?? KeyCode.LeftShift;
            ControllerButton = accelerationControl?.ControllerButton ?? Util.GetKeyCode("RB");

            if (!float.TryParse(settings.GetSetting("acceleration"), out acceleration))
                acceleration = 20f;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyboardKey) || Input.GetKey(ControllerButton))
                PlayerMovement.Singleton.currentSpeed += acceleration;
        }
    }
}
