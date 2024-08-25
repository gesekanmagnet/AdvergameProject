using UnityEngine;
using UnityEngine.EventSystems;

namespace Beyaka.Player
{
    public class InputHandle : MonoBehaviour
    {
        private bool canPlay = true;

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

        private void OnEnable()
        {
            GameController.Instance.OnGameOver += DisableInput;
        }

        private void OnDisable()
        {
            GameController.Instance.OnGameOver -= DisableInput;
        }

        private void DisableInput()
        {
            canPlay = false;
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
            if(canPlay)
            GameController.Instance.OnClick();
        }
    }
}