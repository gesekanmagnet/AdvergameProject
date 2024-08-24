using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FirebaseController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GoogleSignIn();

    public TMP_Text welcomeText;
    public GameObject loginButton;
    public GameObject playButton;

    public void OnLoginButtonPressed()
    {
        // Panggil fungsi GoogleSignIn di JavaScript
        GoogleSignIn();
    }

    public void OnGoogleSignInSuccess(string userName)
    {
        //SceneManager.LoadScene(1);
        loginButton.SetActive(false);
        playButton.SetActive(true);
    }

    public void OnGoogleSignInFail(string errorMessage)
    {
        welcomeText.text = "Login gagal: " + errorMessage;
    }
}