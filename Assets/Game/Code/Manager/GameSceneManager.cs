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
            if (GameController.Instance.GetScore() < FirebaseController.Instance.currentScore)
                return;
           
            FirebaseController.Instance.SaveCurrentScore(GameController.Instance.GetScore());
        }

        public void LoadScene(int x)
        {
            if (x == 1)
                FirebaseController.Instance.SaveCurrentBattery(FirebaseController.Instance.currentBattery - 1);

            SceneManager.LoadScene(x);
        }
    }
}