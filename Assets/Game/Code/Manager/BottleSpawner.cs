using UnityEngine;

namespace Beyaka.Manager
{
    public class BottleSpawner : MonoBehaviour
    {
        [SerializeField] private Transform capit;
        [SerializeField] private SpriteRenderer capitSprite;
        [SerializeField] private GameObject bottlePrefab;
        [SerializeField] private Sprite[] boxIcons;

        private int randomValue;

        private void OnEnable()
        {
            GameController.Instance.OnClick += SpawnBottle;

            SpriteBoxLoop();
        }

        private void OnDisable()
        {
            GameController.Instance.OnClick -= SpawnBottle;
        }

        private void SpawnBottle()
        {
            GameObject bottle = Instantiate(Box(), capit.position, Quaternion.identity);

            SpriteBoxLoop();
        }

        private void SpriteBoxLoop()
        {
            randomValue = RandomValue();
            capitSprite.sprite = boxIcons[randomValue];
        }

        private GameObject Box()
        {
            GameObject box = bottlePrefab;
            SpriteRenderer sprite = box.GetComponent<SpriteRenderer>();
            sprite.sprite = boxIcons[randomValue];
            return box;
        }

        private int RandomValue()
        {
            int x = Random.Range(0, boxIcons.Length);
            return x;
        }
    }
}