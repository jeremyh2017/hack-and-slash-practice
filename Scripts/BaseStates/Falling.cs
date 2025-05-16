using UnityEngine;

public class Falling : State
{
    public Falling(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Falling State");
    }
    public override void Update()
    {
        Debug.Log("Updating Falling State");

        if (StateMachine.PlayerIsGrounded())
        {
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
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Falling State");
    }
}
