using UnityEngine;

public class Running : State
{
    public Running(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Running State");
    }
    public override void Update()
    {
        Debug.Log("Updating Running State");

        if (StateMachine.PlayerIsJumping())
        {
            StateMachine.TryChangeState<Jumping>();
        }
        else if (StateMachine.PlayerIsFalling())
        {
            StateMachine.TryChangeState<Falling>();
        }
        else if (StateMachine.PlayerIsMoving() && !StateMachine.PlayerIsRunning())
        {
            StateMachine.TryChangeState<Walking>();
        }
        else if (!StateMachine.PlayerIsMoving())
        {
            StateMachine.TryChangeState<Idle>();
        }
        
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Running State");
    }
}
