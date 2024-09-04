using UnityEngine;

namespace Beyaka.Bottle
{
    public abstract class BottleState
    {
        public abstract void Enter(BottleBase bottle);
        public abstract void Exit(BottleBase bottle);
        public abstract void OnCollide(BottleBase bottle, Collision2D collision);
        public abstract void OnTrigger(BottleBase bottle, Collider2D collider);
    }
}