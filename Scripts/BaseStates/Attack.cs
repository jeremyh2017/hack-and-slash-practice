using UnityEngine;

public class Attack : State
{
    public CombatEngine    CombatEngine        { get; private set; }

    public Attack(StateMachine stateMachine, CombatEngine combatEngine) : base(stateMachine)
    {
        CombatEngine = combatEngine;
    }

    public override void Enter()
    {
        
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
