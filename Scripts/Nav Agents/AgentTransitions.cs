using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTransitions
{
    private Dictionary<AgentAlphabet, BaseState> _transitions;

    public AgentTransitions(Dictionary<AgentAlphabet, BaseState> transitions)
    {
        _transitions = transitions;
    }

    public BaseState GetNextState(BaseState callingState, AgentAlphabet keyWord)
    {
        if (_transitions.ContainsKey(keyWord))
        {
            return _transitions[keyWord];
        }
        else
        {
            Debug.LogWarning($"Transition using {keyWord} was not declared.");
            return callingState;
            //throw new Exception($"Transition using {keyWord} was not declared.");
        }
    }
}
