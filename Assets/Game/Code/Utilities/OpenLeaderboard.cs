using UnityEngine;

namespace Beyaka.Utilities
{
    public class OpenLeaderboard : MonoBehaviour
    {
        private void OnEnable()
        {
            FirebaseController.Instance.GetLeaderboard();
        }
    }
}