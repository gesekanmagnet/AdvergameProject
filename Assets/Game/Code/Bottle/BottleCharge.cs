using UnityEngine;

namespace Beyaka.Bottle
{
    public class BottleCharge : BottleState
    {
        public override void Enter(BottleBase bottle)
        {
            bottle.transform.parent = null;
        }

        public override void Exit(BottleBase bottle)
        {
            throw new System.NotImplementedException();
        }

        public override void OnCollide(BottleBase bottle, Collision2D collision)
        {
            if (collision.collider.CompareTag(bottle.tag))
            {
                bottle.SwitchState(bottle.bottleInstalled);
                return;
            }
            else
                if (bottle.skip) return;
            
            GameController.Instance.OnGameOver();
        }

        public override void OnTrigger(BottleBase bottle, Collider2D collider)
        {
        }
    }
}