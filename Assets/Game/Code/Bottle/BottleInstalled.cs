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

            GameController.Instance.AddScore();

            if (bottle.skip) return;

            if(collision.collider.CompareTag(bottle.tag) == false)
            {
                GameController.Instance.OnGameOver();
                return;
            }
            GameController.Instance.Up();
        }

        public override void OnTrigger(BottleBase bottle, Collider2D collider)
        {
            if (collider.gameObject == bottle.gameObject) return;
            Debug.Log(collider.gameObject.name);
            Rigidbody2D rb = bottle.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
}