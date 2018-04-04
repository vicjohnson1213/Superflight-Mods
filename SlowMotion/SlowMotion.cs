using SFMF;
using System;
using UnityEngine;

namespace SlowMotion
{
    public class SlowMotion : IMod
    {
        private void FixedUpdate()
        {
            var slow = Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.LeftShift);

            if (slow)
                Time.timeScale = Math.Max(0.4f, Time.timeScale - 0.025f);
            else
                Time.timeScale = Math.Min(1f, Time.timeScale + 0.025f);

            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }
}
