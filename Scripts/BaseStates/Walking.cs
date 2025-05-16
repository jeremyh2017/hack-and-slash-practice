using UnityEngine;

public class Walking : State
{
    public Walking(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Walking State");
    }
    public override void Update()
    {
        Debug.Log("Updating Walking State");

        if (StateMachine.PlayerIsJumping())
        {
            StateMachine.TryChangeState<Jumping>();
        }
        else if (StateMachine.PlayerIsRunning())
        {
            StateMachine.TryChangeState<Running>();
        }
        else if (!StateMachine.PlayerIsMoving())
        {
            StateMachine.TryChangeState<Idle>();
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Walking State");
    }
}
