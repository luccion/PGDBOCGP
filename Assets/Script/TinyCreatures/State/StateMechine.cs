using System;
using UnityEngine;


public enum CreatureState
{
    BATTLE,
    IDLE,
    RUN,
    JUMP
}
/// <summary>
/// FSM 状态机
/// </summary>
[Serializable]
public class StateMachine
{
    public IGameState CurrentState { get; private set; }
    public CreatureState currentState;
    // reference to the state objects
    public RunningState runState;
    public JumpState jumpState;

    // event to notify other objects of the state change
    public event Action<IGameState> stateChanged;

    // pass in necessary parameters into constructor 
    public StateMachine(CreatureController player)
    {
        // create an instance for each state and pass in PlayerController
        runState = new RunningState(player);
        jumpState = new JumpState(player);
        // this.idleState = new IdleState(player);
    }

    // set the starting state
    public void Initialize(CreatureState state)
    {

        CurrentState = SwitchState(state);
        CurrentState.EnterState();

        // notify other objects that state has changed
        stateChanged?.Invoke(CurrentState);
    }

    // exit this state and enter another
    public void TransitionTo(IGameState nextState)
    {
        CurrentState.ExitState();
        CurrentState = nextState;
        nextState.EnterState();

        // notify other objects that state has changed
        stateChanged?.Invoke(nextState);
    }
    public void TransitionTo(CreatureState nextStateName)
    {
        Debug.Log("enter" + nextStateName);
        IGameState nextState = SwitchState(nextStateName);
        CurrentState.ExitState();
        CurrentState = nextState;
        nextState.EnterState();
        currentState = nextStateName;
        // notify other objects that state has changed
        stateChanged?.Invoke(nextState);
    }
    IGameState SwitchState(CreatureState gameState)
    {
        switch (gameState)
        {
            case CreatureState.RUN:
                return runState;
            case CreatureState.JUMP:
                return jumpState;
            default:
                return null;
        }
    }
    // allow the StateMachine to update this state
    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState();
        }
    }
}

