using System.Collections.Generic;
using System;
using UnityEngine;

public class CachedStates
{
    public  StateMachine            StateMachine    { get; private set; }
    private Dictionary<Type, State>     Cache               = new Dictionary<Type, State>();
    public  State                       DefaultState        { get; private set; }

    public  CachedStates(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
        InitializeStates();
    }
    private void InitializeStates()
    {
        Register<Idle>(isDefault: true);
        Register<Walking>();
        Register<Running>();
        Register<Jumping>();
        Register<Falling>();
        Register<CrouchedIdle>();
        Register<CrouchedWalking>();
    }
    private void Register<T>(bool isDefault = false) where T : State
    {
        var state = (T)Activator.CreateInstance(typeof(T), StateMachine) as State;
        Cache[typeof(T)] = state;

        if (isDefault)
            DefaultState = state;
    }

    public State LoadState<T>() where T : State => Cache[typeof(T)];
}
