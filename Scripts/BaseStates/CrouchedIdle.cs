using UnityEngine;

public class CrouchedIdle : State
{
    public CrouchedIdle(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering CrouchedIdle State");
    }
    public override void Update()
    {
        Debug.Log("Updating CrouchedIdle State");

        if (!StateMachine.MovementFlags.CrouchIsActive)
        {
            StateMachine.TryChangeState<Idle>();
        }
        else
        {
            if (StateMachine.PlayerIsJumping())
            {
                StateMachine.TryChangeState<Jumping>();
            }
            else if (StateMachine.PlayerIsRunning())
            {
                StateMachine.TryChangeState<Running>();
            }
            else if (StateMachine.PlayerIsMoving())
            {
                StateMachine.TryChangeState<CrouchedWalking>();
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting CrouchedIdle State");
    }
}
