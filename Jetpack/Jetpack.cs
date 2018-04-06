using SFMF;
using System;
using System.IO;
using UnityEngine;

namespace Jetpack
{
    public class Jetpack : IMod
    {
        private const string SettingsPath = @".\SFMF\ModSettings\Jetpack.csv";

        private float Acceleration { get; set; }
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

                if (parts[0] == "Setting")
                    Acceleration = float.Parse(parts[2]);
                else
                {
                    KeyboardKey = (KeyCode)Enum.Parse(typeof(KeyCode), parts[2]);
                    ControllerButton = GetControllerButton(parts[3]);
                }
            }
        }

        private void FixedUpdate()
        {
            var boost = Input.GetKey(ControllerButton) || Input.GetKey(KeyboardKey);

            if (boost)
            {
                PlayerMovement.Singleton.currentSpeed += Acceleration;
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
