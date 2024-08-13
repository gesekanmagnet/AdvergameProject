using Unity.VisualScripting;
using UnityEngine;

namespace Beyaka.Bottle
{
    public class BottleBase : MonoBehaviour
    {
        [SerializeField] private bool _skip;

        private BottleState currentState;
        public BottleCharge bottleCharge { get; } = new BottleCharge();
        public BottleInstalled bottleInstalled { get; } = new BottleInstalled();
        public BottleFall bottleFall { get; } = new BottleFall();

        public bool skip { get { return _skip; } }

        private void Start()
        {
            currentState = bottleInstalled;
            currentState.Enter(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            currentState.OnCollide(this, collision);
        }

        public void SwitchState(BottleState state)
        {
            currentState = state;
            //state.Enter(this);
        }
    }
}