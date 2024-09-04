using UnityEngine;

namespace Beyaka.Utilities
{
    public class SetDifficulty : MonoBehaviour
    {
        public void PilihMode(bool x)
        {
            FirebaseController.Instance.SetMode(x);
        }
    }
}