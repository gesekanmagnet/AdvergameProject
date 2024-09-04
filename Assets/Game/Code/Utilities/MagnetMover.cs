using UnityEngine;

using DG.Tweening;

namespace Beyaka.Utilities
{
    public class ObjectMove : MonoBehaviour
    {
        [SerializeField] private float minSpeed = 1f; // Kecepatan minimum
        [SerializeField] private float maxSpeed = 3f; // Kecepatan maksimum

        private void Start()
        {
            // Mulai pergerakan bolak-balik
            MoveBackAndForth();
        }

        private void MoveBackAndForth()
        {
            // Dapatkan kecepatan acak antara minSpeed dan maxSpeed
            float randomSpeed = Random.Range(minSpeed, maxSpeed);

            // Gerakkan dari titik A ke titik B
            transform.DOMoveX(2, randomSpeed)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // Setelah mencapai titik B, balikkan gerakan dari titik B ke titik A
                    transform.DOMoveX(-2, randomSpeed)
                        .SetEase(Ease.Linear)
                        .OnComplete(MoveBackAndForth); // Panggil kembali fungsi ini untuk loop
                });
        }
    }
}