using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStateMachine
{
    private BaseState _currentState;
    public BaseState CurrentState => _currentState;

    private Dictionary<BaseState,AgentTransitions> _transitions;
    public AgentStateMachine(Dictionary<BaseState, AgentTransitions> transitions, BaseState startState)
    {
        _transitions = transitions;
        _currentState = startState;
    }

    public void StartMachine()
    {
        _currentState.Enter();
    }
    
    public void SwitchState(AgentAlphabet key)
    {
        _currentState.Exit();
        _currentState = _transitions[_currentState].GetNextState(_currentState,key);
        _currentState.Enter();
    }

}
