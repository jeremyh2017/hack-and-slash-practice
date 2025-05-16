using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public MovementFlags                MovementFlags           { get; private set; }
    public CombatFlags                  CombatFlags             { get; private set; }
    public CharacterController          CharacterController     { get; private set; }
    public Animator                     Animator                { get; private set; }
    public CachedStates                 CachedStates            { get; private set; }
    public State                        CurrentState            { get; private set; }
    public GroundDetection              GroundDetection         { get; private set; }




    public StateMachine(MovementFlags movementFlags, CombatFlags combatFlags, 
                                                     CharacterController characterController,
                                                     Animator animator,
                                                     GroundDetection groundDetection)
    {
        MovementFlags       =       movementFlags;
        CombatFlags         =       combatFlags;
        CharacterController =       characterController;
        Animator            =       animator;
        GroundDetection     =       groundDetection;
    }

    public void InitializeCachedStates()
    {
        if (CachedStates != null)
        {
            Debug.LogWarning("CachedStates already initialized.");
            return;
        }
        CachedStates = new CachedStates(this);
        ChangeState(LoadCachedState<Idle>());
    }

    public bool PlayerIsGrounded()
    {
        return MovementFlags.IsGrounded;
    }

    public bool PlayerIsMoving()
    {
        return MovementFlags.MoveInput.sqrMagnitude > 0;
    }

    public bool PlayerIsRunning()
    {
        if (PlayerIsMoving() && MovementFlags.RunIsPressed)
            return true;
        else
            return false;
    }
    public bool PlayerIsCrouching()
    {
        return MovementFlags.CrouchIsActive;
    }

    public bool PlayerIsJumping()
    {
        return MovementFlags.JumpWasPressed;
    }

    public bool PlayerIsFalling()
    {
        return !MovementFlags.IsGrounded && MovementFlags.VerticalVelocity < 0;
    }

    public bool PlayerIsAttacking()
    {
        return CombatFlags.IsAttacking;
    }

    public void TryChangeState<T>() where T : State
    {
        if (!(CurrentState is T))
        {
            ChangeState(LoadCachedState<T>());
        }
    }

    public void TryEnterAttackState()
    {
        if (CurrentState is Attack)
        {
            return;
        }
        else
        {
            ChangeState(new Attack(this, CombatEngine));
        }
    }

    public void ChangeState(State newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
        else
        {
            CurrentState = CachedStates.DefaultState;
        }
    }

    public State LoadCachedState<T>() where T : State
    {
        return CachedStates.LoadState<T>();
    }

    public void HandleState()
    {
        CurrentState.Update();
    }
}
