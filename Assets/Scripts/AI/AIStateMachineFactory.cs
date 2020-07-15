using TopDownShooter.States;
using UnityEngine;

namespace TopDownShooter
{
    public static class AIStateMachineFactory
    {
        public static StateMachine CreateDefaultStateMachine(Character character)
        {
            var player = GameObject.FindObjectOfType<PlayerCharacterController>().Character;

            var idleState = new IdleState(character, player);

            var followState = new FollowPlayerState(character, player);
            var shootRifleState = new ShootRifleToPlayerState(character, player);
            var attackSubmachineExitState = new StateMachine.ExitState();

            var followToShootRifle = new DelegateCheckTransition<TopDownStateBase, TopDownStateBase>(
                (owner, target) =>
                {
                    var distance = (owner.Character.transform.position - owner.Player.transform.position).magnitude;
                    return distance < 7.0f;
                }, followState, shootRifleState);

            var shootRifleToExit = new DelegateCheckTransition<TopDownStateBase, IState>(
                (owner, target) =>
                {
                    var distance = (owner.Character.transform.position - owner.Player.transform.position).magnitude;
                    return distance > 12.0f;
                }, shootRifleState, attackSubmachineExitState);

            var attackSubmachine = StateMachine.Create(followState, followToShootRifle, shootRifleToExit);
            var attackSubmachineState = new SubmachineState(attackSubmachine);

            var idleToAttack = new PlayerDetectedTransition(true, character, player, idleState, attackSubmachineState);
            var attcakToIdle = new StateFinishedTransition(attackSubmachineState, idleState);

            return StateMachine.Create(idleState, idleToAttack, attcakToIdle);
        }
    }
}
