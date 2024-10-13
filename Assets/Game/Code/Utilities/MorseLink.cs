using UnityEngine;

namespace Beyaka.Utilities
{
    public class MorseLink : MonoBehaviour
    {
        public void OpenLink(string url)
        {
            FirebaseController.Instance.OpenNewTab(url);
        }
    }
}