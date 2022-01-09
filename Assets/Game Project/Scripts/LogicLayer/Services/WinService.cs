using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine.SceneManagement;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public sealed class WinService: IWinService
    {
        public void CatWin()
        {
            SceneManager.LoadScene("CatWinScene");
        }

        public void EngineerWin()
        {
            SceneManager.LoadScene("EngineerWinScene");
        }
    }
}