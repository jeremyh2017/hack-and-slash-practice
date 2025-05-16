using UnityEngine;

public abstract class State
{
    public StateMachine         StateMachine        { get; private set; }

    public State(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        /*StateMachine.Animator.SetTrigger(GetType().Name);*/
        StateMachine.Animator.CrossFade(GetType().Name, 0.15f);
    }

    public virtual void Exit()
    {
        /*StateMachine.Animator.ResetTrigger(GetType().Name);*/
    }

    public abstract void Update();

}
