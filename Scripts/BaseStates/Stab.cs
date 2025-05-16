using UnityEngine;

public class Stab : Attack
{
    public Stab(StateMachine stateMachine, CombatEngine combatEngine) : base(stateMachine, combatEngine) { }

    public override void Enter()
    {
        StateMachine.Animator.CrossFade("StabAttack", 0.15f);
        Debug.Log("Entering Attack State");
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Attack State");
    }
}
