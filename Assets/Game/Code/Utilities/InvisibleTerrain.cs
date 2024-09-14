using UnityEngine;

namespace Beyaka.Manager
{
    public class InvisibleTerrain : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
                GameController.Instance.OnGameOver();
        }
    }
}