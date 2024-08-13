using UnityEngine;
using UnityEngine.SceneManagement;

namespace Beyaka.Manager
{
    public class GameSceneManager : MonoBehaviour
    {
        private void OnEnable()
        {
            GameController.Instance.OnGameOver += RestartGame;
        }

        private void OnDisable()
        {
            GameController.Instance.OnGameOver -= RestartGame;
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}