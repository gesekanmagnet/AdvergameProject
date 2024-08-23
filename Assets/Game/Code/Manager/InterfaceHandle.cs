using UnityEngine;

using TMPro;

namespace Beyaka.Manager
{
    public class InterfaceHandle : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private void OnEnable()
        {
            GameController.Instance.OnScoring += UpdateUI;
        }

        private void OnDisable()
        {
            GameController.Instance.OnScoring -= UpdateUI;
        }

        private void UpdateUI()
        {
            scoreText.text = "SCORE" +
                ": " + GameController.Instance.Score;
        }
    }
}