using System.Runtime.InteropServices;
using UnityEngine;

public class FirebaseController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GoogleSignIn();

    [DllImport("__Internal")]
    private static extern void SaveScore(int score);

    [DllImport("__Internal")]
    private static extern void GetScore();

    public delegate void LoginAction(string userName);
    public LoginAction OnLoginSuccess = delegate { };
    public LoginAction OnLoginFailure = delegate { };

    public static FirebaseController Instance { get; private set; }

    public string currentActiveUsername { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnLoginButtonPressed()
    {
        // Panggil fungsi GoogleSignIn di JavaScript
        GoogleSignIn();
    }

    public void OnGoogleSignInSuccess(string userName)
    {
        OnLoginSuccess(userName);
        currentActiveUsername = userName;
        //SceneManager.LoadScene(1);
        //loginButton.SetActive(false);
        //playButton.SetActive(true);
    }

    public void OnGoogleSignInFail(string errorMessage)
    {
        //welcomeText.text = "Login gagal: " + errorMessage;
        OnLoginFailure(errorMessage);
    }

    public void SaveCurrentScore(int score)
    {
        SaveScore(score);
    }
}