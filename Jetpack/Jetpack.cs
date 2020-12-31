using SFMF;
using System;
using System.IO;
using UnityEngine;

namespace Jetpack
{
    public class Jetpack : IMod
    {
        private const string SettingsPath = @".\SFMF\ModSettings\Jetpack.csv";

        private float BoostStrength { get; set; }
        private float BreakStrength { get; set; }
        private KeyCode KeyboardKeyBoost { get; set; }
        private KeyCode ControllerButtonBoost { get; set; }
        private KeyCode KeyboardKeyBreak { get; set; }
        private KeyCode ControllerButtonBreak { get; set; }

        private void Start()
        {
            var settings = File.ReadAllLines(SettingsPath);

            foreach (var line in settings)
            {
                var parts = line.Split(',');

                if (parts[0] == "Setting")
                {
                    if (parts[1] == "BoostStrength")
                        BoostStrength = float.Parse(parts[2]);
                    if (parts[1] == "BreakStrength")
                        BreakStrength = float.Parse(parts[2]);
                }
                if (parts[0] == "Control")
                {
                    if (parts[1] == "Boost")
                    {
                        KeyboardKeyBoost = (KeyCode)Enum.Parse(typeof(KeyCode), parts[2]);
                        ControllerButtonBoost = GetControllerButton(parts[3]);
                    }
                    if (parts[1] == "Break")
                    {
                        KeyboardKeyBreak = (KeyCode)Enum.Parse(typeof(KeyCode), parts[2]);
                        ControllerButtonBreak = GetControllerButton(parts[3]);
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            var boost = Input.GetKey(ControllerButtonBoost) || Input.GetKey(KeyboardKeyBoost);
            var brk   = Input.GetKey(ControllerButtonBreak) || Input.GetKey(KeyboardKeyBreak);

            if (boost)
                PlayerMovement.Singleton.currentSpeed += BoostStrength;
            if (brk)
                PlayerMovement.Singleton.currentSpeed -= BreakStrength;
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
