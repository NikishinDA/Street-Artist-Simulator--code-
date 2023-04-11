using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentState
{
    public void Enter();
    public void Exit();
    public void StateUpdate();

    //public IAgentState GetNextState(AgentAlphabet key);
}

public interface IAgentStateFixedExt
{
    public void StateFixedUpdate();
}

public interface IAgentStateLateExt
{
    public void StateLateUpdate();
}

public interface IAgentStateMainInvokeExt
{
    public void StateInvokeUpdate();
}
