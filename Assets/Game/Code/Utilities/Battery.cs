using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Beyaka.Manager;

namespace Beyaka.Utilities
{
    public class Battery : MonoBehaviour
    {
        [SerializeField] private TweenerStack tweenerStack;
        [SerializeField] private GameObject battery;
        [SerializeField] private Transform stackConfirm, stackNotConfirm;

        [SerializeField] private TMP_Text sisaBatteryText;

        [SerializeField] private string password3Battery, password99Battery;

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

        public void RechargeBattery(InputField input)
        {
            if(input.text == password3Battery)
                FirebaseController.Instance.SaveCurrentBattery(3);

            if(input.text == password99Battery)
                FirebaseController.Instance.SaveCurrentBattery(99);

            input.text = string.Empty;
            battery.SetActive(false);
            battery.SetActive(true);
        }
    }
}