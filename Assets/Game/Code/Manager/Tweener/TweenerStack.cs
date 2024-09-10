using UnityEngine;

using DG.Tweening;

namespace Beyaka.Manager
{
    public class TweenerStack : MonoBehaviour
    {
        [SerializeField] private float center, duration;
        [SerializeField] private Transform firstpos, point;

        private Vector3 startPos;

        private void Start()
        {
            startPos = firstpos.position;
        }

        public void popup(Transform popup)
        {
            popup.DOMove(point.position, duration).SetEase(Ease.InExpo);
        }
        public void BackPopup(Transform popup)
        {
            popup.DOMove(startPos, duration).SetEase(Ease.InExpo);
        }

    }
}
