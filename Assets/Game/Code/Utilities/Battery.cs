using UnityEngine;
using TMPro;

using Beyaka.Manager;
using UnityEngine.UI;

namespace Beyaka.Utilities
{
    public class Battery : MonoBehaviour
    {
        [SerializeField] private TweenerStack tweenerStack;
        [SerializeField] private Transform stackConfirm, stackNotConfirm;

        [SerializeField] private TMP_Text sisaBatteryText;

        private void OnEnable()
        {
            if(GameController.Instance != null)
                GameController.Instance.OnGameOver += GameplayBatteryCheck;
        }

        private void OnDisable()
        {
            if(GameController.Instance != null)
                GameController.Instance.OnGameOver -= GameplayBatteryCheck;
        }

        public void MainBatteryCheck()
        {
            tweenerStack.popup(FirebaseController.Instance.currentBattery > 0 ? stackConfirm : stackNotConfirm);
        }

        private void GameplayBatteryCheck()
        {
            sisaBatteryText.text = "Sisa Battery " + FirebaseController.Instance.currentBattery + "/3";
        }
    }
}