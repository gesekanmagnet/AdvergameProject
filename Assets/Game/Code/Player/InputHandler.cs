using UnityEngine;
using UnityEngine.EventSystems;

namespace Beyaka.Player
{
    public class InputHandler : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (IsPointerOverUI())
                {
                    // Jika tap terjadi pada UI, abaikan input untuk gameplay
                    return;
                }

                // Lanjutkan dengan logika gameplay tap
                HandleTap();
            }

            // Pengecekan untuk perangkat mobile atau touch input
            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //{
            //    if (IsPointerOverUI())
            //    {
            //        // Jika tap terjadi pada UI, abaikan input untuk gameplay
            //        return;
            //    }

            //    // Lanjutkan dengan logika gameplay tap
            //    HandleTap();
            //}
        }

        private bool IsPointerOverUI()
        {
            // Untuk mouse di desktop
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }

            // Untuk sentuhan di perangkat mobile
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }

            return false;
        }

        private void HandleTap()
        {
            GameController.Instance.OnClick();
        }
    }
}