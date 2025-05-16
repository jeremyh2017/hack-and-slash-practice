using UnityEngine;

public class Idle : State
{
    public Idle(StateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        /*base.Enter();*/
        if (StateMachine.CombatFlags.IsArmed)
            StateMachine.Animator.CrossFade("CombatIdle", 0.15f);
        else
            StateMachine.Animator.CrossFade("Idle", 0.15f);

        Debug.Log("Entering Idle State");
    }
    public override void Update()
    {
        if (StateMachine.PlayerIsAttacking())
            StateMachine.TryEnterAttackState();

        if (StateMachine.PlayerIsJumping())
            StateMachine.TryChangeState<Jumping>();

        else if (StateMachine.PlayerIsRunning())
            StateMachine.TryChangeState<Running>();

        else if (StateMachine.PlayerIsCrouching())
            StateMachine.TryChangeState<CrouchedIdle>();

        else if (StateMachine.PlayerIsMoving())
            StateMachine.TryChangeState<Walking>();
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Idle State");
    }
}
