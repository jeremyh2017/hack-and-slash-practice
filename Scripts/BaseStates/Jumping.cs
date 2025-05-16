using UnityEngine;

public class Jumping : State
{
    public Jumping(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Jumping State");
    }
    public override void Update()
    {
        Debug.Log("Updating Jumping State");

        if (StateMachine.PlayerIsFalling())
        {
            StateMachine.TryChangeState<Falling>();
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Jumping State");
    }
}
