using System.Collections.Generic;
using UnityEngine;

public class CombatEngine
{
    public CombatFlags      CombatFlags         { get; private set; }
    public StateMachine     StateMachine        { get; private set; }
    public Queue<Attack>    AttackQueue         { get; private set; } = new Queue<Attack>();

    public CombatEngine(CombatFlags combatFlags, StateMachine stateMachine)
    {
        CombatFlags     = combatFlags;
        StateMachine    = stateMachine;
    }
    public void HandleCombatInput()
    {
        if (CombatFlags.DrawWeaponPressed)
        {
            if (!CombatFlags.IsArmed)
            {
                DrawWeapon();
            }
            else
            {
                SheatheWeapon();
            }
            
            CombatFlags.SetDrawWeaponPressed(false);            
        }

        if (CombatFlags.StabAttackPressed)
        {
            EnqueueAttack(new Stab(StateMachine, this));
            Debug.Log("Stab Attack Pressed");
            CombatFlags.SetStabAttackPressed(false);
        }

        if (CombatFlags.SlashAttackPressed)
        {
            EnqueueAttack(new Slash(StateMachine, this));
            CombatFlags.SetSlashAttackPressed(false);
        }

        if (AttackQueue.Count > 0)
        {
            CombatFlags.SetIsAttacking(true);
        }
    }
    public void DrawWeapon()
    {
        StateMachine.Animator.CrossFade("DrawWeapon", 0.15f);
        CombatFlags.SetIsArmed(true);
        Debug.Log("Weapon Drawn");
    }

    public void SheatheWeapon()
    {
        //Animator.CrossFade("SheatheWeapon", 0.1f);
        CombatFlags.SetIsArmed(false);
        Debug.Log("Weapon Sheathed");
    }

    private void EnqueueAttack(Attack attack)
    {
        AttackQueue.Enqueue(attack);
        Debug.Log("Attack Enqueued");
    }

    public void DequeueAttack()
    {
        if (AttackQueue.Count > 0)
        {
            Attack attack = AttackQueue.Dequeue();
            StateMachine.ChangeState(attack);
            Debug.Log("Attack Dequeued");
        }
        else
        {
            Debug.Log("No attacks in queue");
        }
    }
}
