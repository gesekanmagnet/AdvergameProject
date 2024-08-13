using UnityEngine;

namespace Beyaka.Bottle
{
    public class BottleInstalled : BottleState
    {
        public override void Enter(BottleBase bottle)
        {
            //bottle.transform.parent = null;
        }

        public override void Exit(BottleBase bottle)
        {
            throw new System.NotImplementedException();
        }

        public override void OnCollide(BottleBase bottle, Collision2D collision)
        {
            bottle.SwitchState(bottle.bottleFall);

            if (bottle.skip) return;

            if(collision.collider.CompareTag(bottle.tag) == false)
            {
                GameController.Instance.OnGameOver();
                return;
            }
            GameController.Instance.Up();
        }
    }
}