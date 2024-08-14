using System.Threading.Tasks;
using Firebase.Extensions;
using Google;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    public string GoogleAPI = "YOUR_API_KEY";

    private GoogleSignInConfiguration configuration;

    //Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public TMP_Text Username;

    public GameObject LoginScreen, ProfileScreen;

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = GoogleAPI,
            RequestIdToken = true,
        };
    }

    private void Start()
    {
        InitFirebase();
        Debug.LogWarning("MANA");
    }

    void InitFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void GoogleSignInClick()
    {
        Debug.LogError("mantap");
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    public void Logout()
    {
        // Sign out dari Firebase
        auth.SignOut();

        // Sign out dari Google
        GoogleSignIn.DefaultInstance.SignOut();

        // Reset UI untuk menunjukkan layar login
        LoginScreen.SetActive(true);
        ProfileScreen.SetActive(false);

        Debug.Log("User has been logged out.");
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        Debug.LogError("Login mang");
        if (task.IsFaulted)
        {
            Debug.LogError("Faulted");
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Cancelled");
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
                if (task.IsCanceled)
                {
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                user = auth.CurrentUser;

                Username.text = user.DisplayName;
                //UserEmail.text = user.Email;

                LoginScreen.SetActive(false);
                ProfileScreen.SetActive(true);

                // StartCoroutine(LoadImage(CheckImageUrl(user.PhotoUrl.ToString())));
            });
        }
    }

    // private string CheckImageUrl(string url) {
    //     if (!string.IsNullOrEmpty(url)) {
    //         return url;
    //     }
    //     return imageUrl;
    // }

    // IEnumerator LoadImage(string imageUri) {
    //     WWW www = new WWW(imageUri);
    //     yield return www;

    //     UserProfilePic.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    // }
}