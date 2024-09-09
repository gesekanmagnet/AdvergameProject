using UnityEngine;

using DG.Tweening;

namespace Beyaka.Manager
{
    public class Tweener : MonoBehaviour
    {
        [SerializeField] private float center, duration;

        [SerializeField] private Transform sk, point;

        private Vector3 startPos;

        [Header("Pesawat 1")]
        [SerializeField] private Transform pos_1;
        [SerializeField] private Transform pos_2;
        [SerializeField] private Transform pesawat;

        [Header("Pesawat 2")]
        [SerializeField] private Transform pos2_1;
        [SerializeField] private Transform pos2_2;
        [SerializeField] private Transform pesawat2;

        private void Start()
        {
            //startPos = sk.position;
            Pesawat(pesawat, pesawat.GetComponent<SpriteRenderer>().flipX, pos_1, pos_2);
            Pesawat(pesawat2, pesawat2.GetComponent<SpriteRenderer>().flipX, pos2_1, pos2_2);
        }

        public void Popup(Transform popup)
        {
            popup.DOMove(point.position, duration).SetEase(Ease.OutBounce);
        }

        public void BackPopup(Transform popup)
        {
            popup.DOMove(startPos, duration).SetEase(Ease.InExpo);
        }

        public void Pesawat(Transform flyingObject, bool flip, Transform pos1, Transform pos2)
        {
            flyingObject.DOMove(pos2.position, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                flyingObject.GetComponent<SpriteRenderer>().flipX = !flip;
                flyingObject.DOMove(pos1.position, duration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    flyingObject.GetComponent<SpriteRenderer>().flipX = flip;
                    Pesawat(flyingObject, flip, pos1, pos2);
                });
            });
        }
    }
}