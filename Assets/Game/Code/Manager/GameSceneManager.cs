using UnityEngine;
using UnityEngine.SceneManagement;

namespace Beyaka.Manager
{
    public class GameSceneManager : MonoBehaviour
    {
        private void OnEnable()
        {
            GameController.Instance.OnGameOver += SaveScore;
        }

        private void OnDisable()
        {
            GameController.Instance.OnGameOver -= SaveScore;
        }

        private void SaveScore()
        {
            FirebaseController.Instance.SaveCurrentScore(GameController.Instance.GetScore());
        }

        public void LoadScene(int x)
        {
            SceneManager.LoadScene(x);
        }
    }
}