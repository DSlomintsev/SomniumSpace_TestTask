using System;
using System.Collections.Generic;
using Common.Infrastructure.States;
using Common.Services;
using UnityEngine;


namespace Common.Infrastructure
{
  public class BaseAppStateMachine : BaseService
  {
    protected readonly Dictionary<Type, IState> States = new ();
    private IState _activeState;

    protected override void OnInit()
    {
      base.OnInit();
      
      //_states.Add(typeof(BootstrapState), new BootstrapState());
    }

    public void Enter<TState>() where TState : class, IState
    {
      Debug.Log("Enter "+typeof(TState).Name);
      var state = ChangeState<TState>();
      state.Enter();
    }

    private TState ChangeState<TState>() where TState : class, IState
    {
      _activeState?.Exit();
      var state = States[typeof(TState)];
      _activeState = state;
      return (TState)state;
    }
  }
}