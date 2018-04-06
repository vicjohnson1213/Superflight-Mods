using SFMF;
using System;
using System.IO;
using UnityEngine;

namespace SlowMotion
{
    public class SlowMotion : IMod
    {
        private const string SettingsPath = @".\SFMF\ModSettings\Slow Motion.csv";

        private KeyCode KeyboardKey { get; set; }
        private KeyCode ControllerButton { get; set; }

        private void Start()
        {
            var settings = File.ReadAllLines(SettingsPath);

            foreach (var line in settings)
            {
                if (line == "")
                    continue;

                var parts = line.Split(',');

                KeyboardKey = (KeyCode)Enum.Parse(typeof(KeyCode), parts[2]);
                ControllerButton = GetControllerButton(parts[3]);
            }
        }

        private void FixedUpdate()
        {
            var slow = Input.GetKey(ControllerButton) || Input.GetKey(KeyboardKey);

            if (slow)
            {
                Time.timeScale = Math.Max(0.6f, Time.timeScale - 0.025f);
                Time.fixedDeltaTime = .02f * .8f;
            }
            else
            {
                Time.timeScale = Math.Min(1f, Time.timeScale + 0.025f);
                Time.fixedDeltaTime = .02f * Time.timeScale;
            }
        }

        private KeyCode GetControllerButton(string button)
        {
            if (button == "B")
                return KeyCode.JoystickButton1;
            if (button == "X")
                return KeyCode.JoystickButton2;
            if (button == "Y")
                return KeyCode.JoystickButton3;
            if (button == "LB")
                return KeyCode.JoystickButton4;
            if (button == "RB")
                return KeyCode.JoystickButton5;
            if (button == "Select")
                return KeyCode.JoystickButton6;
            if (button == "L3")
                return KeyCode.JoystickButton8;
            if (button == "R3")
                return KeyCode.JoystickButton9;

            return KeyCode.None;
        }
    }
}
