using Unity.VisualScripting;
using UnityEngine;

namespace Beyaka.Bottle
{
    public class BottleBase : MonoBehaviour
    {
        [SerializeField] private bool _skip;

        public BottleState currentState { get; private set; }
        public BottleInstalled bottleInstalled { get; } = new BottleInstalled();
        public BottleFall bottleFall { get; } = new BottleFall();

        public bool skip { get { return _skip; } }
        public bool boxBanned { get; set; }

        private void Start()
        {
            currentState = bottleInstalled;
            currentState.Enter(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            currentState.OnCollide(this, collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            currentState.OnTrigger(this, collision);
        }

        public void SwitchState(BottleState state)
        {
            currentState = state;
            //state.Enter(this);
        }
    }
}