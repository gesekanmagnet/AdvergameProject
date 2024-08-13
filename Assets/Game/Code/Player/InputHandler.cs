using UnityEngine;

namespace Beyaka.Player
{
    public class InputHandler : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                GameController.Instance.OnClick();
            }
        }
    }
}