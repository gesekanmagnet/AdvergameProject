using UnityEngine;

namespace Beyaka.Utilities
{
    public class CustomParticle : MonoBehaviour
    {
        public void DisableObject()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}