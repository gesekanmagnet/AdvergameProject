using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Beyaka.Manager
{
    public class MainMenuInterfaceHandle : MonoBehaviour
    {
        [SerializeField] private TMP_Text welcomeText;
        [SerializeField] private GameObject loginButton, resumeButton;
        [SerializeField] private GameObject loginPanel, menuPanel;
        //[SerializeField] private GameObject betaPanel;

        private void Start()
        {
            ShowMainMenu();
        }

        private void OnEnable()
        {
            FirebaseController.Instance.OnLoginSuccess += UpdateUI;
            FirebaseController.Instance.OnLoginFailure += UpdateUIFailure;
        }

        private void OnDisable()
        {
            FirebaseController.Instance.OnLoginSuccess -= UpdateUI;
            FirebaseController.Instance.OnLoginFailure -= UpdateUIFailure;
        }

        private void ShowMainMenu()
        {
            if(IsSignedIn())
            {
                menuPanel.SetActive(true);
                loginPanel.SetActive(false);
                //betaPanel.SetActive(false);
                //UpdateBattery(string.Empty);
            }
            else
            {
                loginPanel.SetActive(true);
                menuPanel.SetActive(false);
                //betaPanel.SetActive(true);
            }
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