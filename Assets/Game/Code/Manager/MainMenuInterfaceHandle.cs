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

        [SerializeField] private TMP_InputField instagramUser;
        
        //[SerializeField] private GameObject betaPanel;

        private void Start()
        {
            ShowMainMenu();
        }

        private void OnEnable()
        {
            FirebaseController.Instance.OnLoginSuccess += UpdateUI;
            FirebaseController.Instance.OnLoginFailure += UpdateUIFailure;
            FirebaseController.Instance.OnUsernameRegister += CheckUsername;
        }

        private void OnDisable()
        {
            FirebaseController.Instance.OnLoginSuccess -= UpdateUI;
            FirebaseController.Instance.OnLoginFailure -= UpdateUIFailure;
            FirebaseController.Instance.OnUsernameRegister -= CheckUsername;
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

        private void CheckUsername(bool x)
        {
            if (x)
            {
                welcomeText.color = Color.red;
                welcomeText.text = "Username telah digunakan";
            }
            else
            {
                welcomeText.color = Color.white;
                welcomeText.text = "Username pengguna tersedia";
            }
        }

        private bool IsSignedIn()
        {
            if(string.IsNullOrEmpty(FirebaseController.Instance.currentActiveUsername))
                return false;

            return true;
        }

        private void UpdateUI(string user)
        {
            if(string.IsNullOrEmpty(user))
            {
                welcomeText.text = "Masukan Username instagram anda, sebagai Username dalam Game";
                instagramUser.gameObject.SetActive(true);
            }
            else
            {
                welcomeText.text = user;
                instagramUser.gameObject.SetActive(false);
            }

            loginButton.SetActive(false);
            resumeButton.SetActive(true);  
        }

        private void UpdateUIFailure(string user)
        {
            welcomeText.text = user;
        }

        public void Resume()
        {
            if (instagramUser.gameObject.activeInHierarchy && string.IsNullOrEmpty(instagramUser.text) || FirebaseController.Instance.usernameExist)
                return;
            else if(instagramUser.gameObject.activeInHierarchy && !string.IsNullOrEmpty(instagramUser.text))
            {
                FirebaseController.Instance.SetUsername(instagramUser.text);
                ShowMainMenu();
            }
            else
                ShowMainMenu();
        }

        public void CheckUsernameValidation(string username)
        {
            if (string.IsNullOrEmpty(instagramUser.text))
            {
                welcomeText.text = "Masukan Username instagram anda, sebagai Username dalam Game";
                return;
            }

            FirebaseController.Instance.IsUsernameValid(username);
        }
    }
}