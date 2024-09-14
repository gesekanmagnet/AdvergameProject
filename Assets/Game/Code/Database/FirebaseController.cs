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

    [DllImport("__Internal")]
    private static extern void SaveBattery(int battery);

    [DllImport("__Internal")]
    private static extern void GetBattery();

    public delegate void LoginAction(string userName);
    public LoginAction OnLoginSuccess { get; set; } = delegate { };
    public LoginAction OnLoginFailure { get; set; } = delegate { };

    public delegate void LeaderboardUpdateAction(List<ScoreEntry> leaderboard);
    public LeaderboardUpdateAction OnLeaderboardUpdate { get; set; } = delegate { };

    public static FirebaseController Instance { get; private set; }

    public string currentActiveUsername { get; private set; } = null;

    public bool easyMode { get; private set; }
    public int currentScore { get; private set; }
    public int currentBattery { get; private set; }

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
        GetCurrentScore();
        GetCurrentBattery();
    }

    public void OnGoogleSignInFail(string errorMessage)
    {
        OnLoginFailure(errorMessage);
    }

    public void SaveCurrentScore(int score)
    {
        SaveScore(score);
        currentScore = score;
    }

    private void GetCurrentScore()
    {
        GetScore();
    }

    public void OnReceiveScore(string score)
    {
        int scoreValue;
        if(int.TryParse(score, out scoreValue))
        {
            currentScore = scoreValue;
        }
        else
        {
            currentScore = 0;
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

    public void SetMode(bool x)
    {
        easyMode = x;
    }

    public void OnReceiveBattery(string battery)
    {
        int batteryValue;
        if (int.TryParse(battery, out batteryValue))
        {
            currentBattery = batteryValue;
        }
        else
        {
            currentBattery = 0;
        }
    }

    private void GetCurrentBattery()
    {
        GetBattery();
    }

    public void SaveCurrentBattery(int battery)
    {
        SaveBattery(battery);
        currentBattery = battery;
    }
}

[System.Serializable]
public class ScoreEntry
{
    public string username;
    public int score;
}