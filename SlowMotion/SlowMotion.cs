using SFMF;
using System;
using UnityEngine;

namespace SlowMotion
{
    public class SlowMotion : IMod
    {
        private void Update()
        {
            var slow = Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.LeftShift);

            if (slow)
                Time.timeScale = Math.Max(0.5f, Time.timeScale - 0.005f);
            else
                Time.timeScale = Math.Min(1f, Time.timeScale + 0.005f);

            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }
}
