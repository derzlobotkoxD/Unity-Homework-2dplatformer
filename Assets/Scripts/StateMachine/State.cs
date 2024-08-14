public abstract class State
{
    protected StateMachine StateMachine;

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public void SetStateMachine(StateMachine stateMachine) =>
        StateMachine = stateMachine;
}