using System.Collections.Generic;

using UnityEngine;
using TMPro;

namespace Beyaka.Manager
{
    public class LeaderboardHandle : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] nameTexts, scoreTexts;

        private void OnEnable()
        {
            FirebaseController.Instance.OnLeaderboardUpdate += UpdateLeaderboardUI;
        }

        private void OnDisable()
        {
            FirebaseController.Instance.OnLeaderboardUpdate -= UpdateLeaderboardUI;
        }

        private void UpdateLeaderboardUI(List<ScoreEntry> leaderboard)
        {
            for (int i = 0; i < nameTexts.Length; i++)
            {
                if (i < leaderboard.Count) // Pastikan ada data untuk ditampilkan
                {
                    nameTexts[i].text = leaderboard[i].username;
                    scoreTexts[i].text = leaderboard[i].score.ToString();
                }
                else
                {
                    // Jika tidak ada data, kosongkan teks
                    nameTexts[i].text = "-";
                    scoreTexts[i].text = "-";
                }
            }
        }
    }
}