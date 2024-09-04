using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;

using Newtonsoft.Json;

public class FirebaseController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GoogleSignIn();

    [DllImport("__Internal")]
    private static extern void SaveScore(int score);

    [DllImport("__Internal")]
    private static extern void GetScore();

    [DllImport("__Internal")]
    private static extern void GetTopScores();

    public delegate void LoginAction(string userName);
    public LoginAction OnLoginSuccess { get; set; } = delegate { };
    public LoginAction OnLoginFailure { get; set; } = delegate { };

    public delegate void LeaderboardUpdateAction(List<ScoreEntry> leaderboard);
    public LeaderboardUpdateAction OnLeaderboardUpdate { get; set; } = delegate { };

    public static FirebaseController Instance { get; private set; }

    public string currentActiveUsername { get; private set; } = null;

    public int currentScore { get; private set; }

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

    public void GetCurrentScore()
    {
        GetScore();
    }

    public void OnReceiveScore(string score)
    {
        int gg;
        if(int.TryParse(score, out gg))
        {
            currentScore = gg;
        }
    }

    public void GetLeaderboard()
    {
        GetTopScores(); // Panggil fungsi JS untuk mendapatkan top skor
    }

    // Fungsi ini dipanggil dari JavaScript untuk memberikan data leaderboard ke Unity
    public void OnReceiveTopScores(string json)
    {
        if (json != "Error")
        {
            var leaderboard = JsonConvert.DeserializeObject<List<ScoreEntry>>(json);
            OnLeaderboardUpdate(leaderboard);
        }
        else
        {
            Debug.LogError("Error fetching leaderboard data.");
        }
    }
}

[System.Serializable]
public class ScoreEntry
{
    public string username;
    public int score;
}