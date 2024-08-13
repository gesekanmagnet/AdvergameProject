using UnityEngine;

namespace Beyaka.Manager
{
    public class BottleSpawner : MonoBehaviour
    {
        [SerializeField] private Transform capit;
        [SerializeField] private GameObject bottlePrefab;

        private void OnEnable()
        {
            GameController.Instance.OnClick += SpawnBottle;
        }

        private void OnDisable()
        {
            GameController.Instance.OnClick -= SpawnBottle;
        }

        private void SpawnBottle()
        {
            GameObject bottle = Instantiate(bottlePrefab, capit.position, Quaternion.identity);
        }
    }
}