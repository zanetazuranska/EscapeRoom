namespace ER
{
    public abstract class BaseState
    {
        StateMachine context;
        public StateMachine Context { get { return context; } set { context = value; } }

        private int id = 0;
        public BaseState(int id) => this.id = id;

        public abstract void EnterState(StateMachine stateMachine);
        public abstract void UpdateState(StateMachine stateMachine);
        public abstract void ExitState(StateMachine stateMachine);

        public int GetStateId()
        {
            return id;
        }
    }
}