using UnityEngine;

namespace TopDownShooter.States
{
    public class IdleState : TopDownStateBase
    {
        public IdleState(Character character, Character player) : base(character, player)
        {
        }

        public override void OnStateEnter(IState fromState)
        {
            base.OnStateEnter(fromState);

            Character.GetComponent<AIMotor>().SetMovementToTarget(Character.transform.position);
        }
    }
}