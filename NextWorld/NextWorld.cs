using SFMF;
using System;
using System.IO;
using UnityEngine;

namespace NextWorld
{
    public class NextWorld : IMod
    {
        private const string SettingsPath = @".\SFMF\ModSettings\Next World.csv";

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

        private void Update()
        {
            var nextPressed = Input.GetKeyDown(KeyboardKey) || Input.GetKeyDown(ControllerButton);
            if (LocalGameManager.Singleton.playerState == LocalGameManager.PlayerState.Flying && nextPressed)
            {
                LocalGameManager.Singleton.ResetPlayer();
                LevelGenerator.Singleton.GenerateLevel(WorldManager.NewWorld());
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
