using UnityEngine;

namespace Beyaka
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;

        public delegate void Action();
        public Action OnGameStart { get; set; } = delegate { };
        public Action OnGameOver { get; set; } = delegate { };
        public Action OnClick { get; set; } = delegate { };

        public static GameController Instance { get; private set; }

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Up()
        {
            transform.position += offset;
        }
    }
}