using SFMF;

namespace FirstPerson
{
    public class FirstPerson : IMod
    {
        private void Start()
        {
        }
        private void Update()
        {
            LocalGameManager.Singleton.playerCamManager.mainCamera.mainCameraReference.transform.position = LocalGameManager.Singleton.player.transform.position;
        }
    }
}
