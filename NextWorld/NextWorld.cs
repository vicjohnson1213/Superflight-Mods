using SFMF;
using UnityEngine;

namespace NextWorld
{
    public class NextWorld : IMod
    {
        private void Update()
        {
            var nextPressed = Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton2);
            if (LocalGameManager.Singleton.playerState == LocalGameManager.PlayerState.Flying && nextPressed)
            {
                LocalGameManager.Singleton.ResetPlayer();
                LevelGenerator.Singleton.GenerateLevel(WorldManager.NewWorld());
            }
        }
    }
}
