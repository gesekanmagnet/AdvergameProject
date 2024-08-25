using UnityEngine;
using TMPro;

namespace Beyaka.Manager
{
    public class MainMenuInterfaceHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text welcomeText;
        [SerializeField] private GameObject loginButton, resumeButton;
        [SerializeField] private GameObject loginPanel, menuPanel;

        private void OnEnable()
        {
            FirebaseController.Instance.OnLoginSuccess += UpdateUI;
            FirebaseController.Instance.OnLoginFailure += UpdateUIFailure;

            ShowMainMenu();
        }

        private void OnDisable()
        {
            FirebaseController.Instance.OnLoginSuccess -= UpdateUI;
            FirebaseController.Instance.OnLoginFailure -= UpdateUIFailure;
        }

        private void ShowMainMenu()
        {
            if(IsSignedIn())
                menuPanel.SetActive(true);
            else
                loginPanel.SetActive(true);
        }

        private bool IsSignedIn()
        {
            if(FirebaseController.Instance.currentActiveUsername != null)
                return true;

            return false;
        }

        private void UpdateUI(string user)
        {
            welcomeText.text = user;
            loginButton.SetActive(false);
            resumeButton.SetActive(true);  
        }

        private void UpdateUIFailure(string user)
        {
            welcomeText.text = user;
        }
    }
}