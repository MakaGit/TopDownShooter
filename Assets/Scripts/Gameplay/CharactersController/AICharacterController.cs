namespace TopDownShooter
{
    public class AICharacterController : CharacterController
    {
        private StateMachine _stateMachine = null;

        private void Start()
        {
            // 1) Создать FSM
            _stateMachine = AIStateMachineFactory.CreateDefaultStateMachine(Character);

            // 2) Инициализировать FSM
            _stateMachine.Start();
        }

        private void Update()
        {
            _stateMachine.Transit();
            _stateMachine.Update();
        }

        private void OnDestroy()
        {
            _stateMachine.Stop();
        }
    }
}
