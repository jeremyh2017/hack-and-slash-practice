using UnityEngine;

public class CrouchedWalking : State
{
    public CrouchedWalking(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering CrouchedWalking State");
    }
    public override void Update()
    {
        Debug.Log("Updating CrouchedWalking State");

        if (!StateMachine.MovementFlags.CrouchIsActive)
        {
            if (StateMachine.PlayerIsJumping())
            {
                StateMachine.TryChangeState<Jumping>();
            }
            if (StateMachine.PlayerIsRunning())
            {
                StateMachine.TryChangeState<Running>();
            }
            else if (StateMachine.PlayerIsMoving())
            {
                StateMachine.TryChangeState<Walking>();
            }
            else
            {
                StateMachine.TryChangeState<Idle>();
            }
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
            else if (!StateMachine.PlayerIsMoving())
            {
                StateMachine.TryChangeState<CrouchedIdle>();
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting CrouchedWalking State");
    }
}
