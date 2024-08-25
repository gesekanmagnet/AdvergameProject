using UnityEngine;

using TMPro;

namespace Beyaka.Manager
{
    public class InterfaceHandle : MonoBehaviour
    {
        [SerializeField] private TMP_Text updateScoreText, finalScoreText;
        [SerializeField] private GameObject scorePanel;

        private void OnEnable()
        {
            GameController.Instance.OnScoring += UpdateUI;
            GameController.Instance.OnGameOver += ShowScore;
        }

        private void OnDisable()
        {
            GameController.Instance.OnScoring -= UpdateUI;
            GameController.Instance.OnGameOver -= ShowScore;
        }

        private void UpdateUI()
        {
            updateScoreText.text = GameController.Instance.GetScore().ToString();
        }

        private void ShowScore()
        {
            finalScoreText.text = GameController.Instance.GetScore().ToString();
            scorePanel.SetActive(true);
        }
    }
}