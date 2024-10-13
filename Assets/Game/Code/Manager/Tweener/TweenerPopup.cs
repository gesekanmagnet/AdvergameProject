using DG.Tweening;
using UnityEngine;

namespace Beyaka.Utilities
{
    public class TweenerPopup : MonoBehaviour
    {
        [SerializeField] private Transform startPoint, endPoint, isi;

        private void OnEnable()
        {
            transform.DOMove(endPoint.position, 1).SetEase(Ease.InOutBack);
        }

        public void GoBack()
        {
            transform.DOMove(startPoint.position, 1).SetEase(Ease.InOutBack);
        }
    }
}