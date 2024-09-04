using UnityEngine;

namespace Beyaka.Bottle
{
    public class BottleFall : BottleState
    {
        public override void Enter(BottleBase bottle)
        {
            bottle.boxBanned = true;
        }

        public override void Exit(BottleBase bottle)
        {
            throw new System.NotImplementedException();
        }

        public override void OnCollide(BottleBase bottle, Collision2D collision)
        {
            if(collision.collider.CompareTag(bottle.tag) == false)
            {
                GameController.Instance.OnGameOver();
            }
            else
            {
                BottleBase bottleCollide = collision.gameObject.GetComponent<BottleBase>();
                if(bottleCollide.boxBanned)
                    GameController.Instance.OnGameOver();
            }
        }

        public override void OnTrigger(BottleBase bottle, Collider2D collider)
        {
        }
    }
}