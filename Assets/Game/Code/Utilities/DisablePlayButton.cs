using UnityEngine;

namespace Beyaka.Utilities
{
    public class DisablePlayButton : MonoBehaviour
    {
        [SerializeField] private GameObject playAgainButton;

        private void OnEnable()
        {
            playAgainButton.SetActive(FirebaseController.Instance.currentBattery > 0 ? true : false);
        }
    }
}