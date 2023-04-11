using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AgentAlphabet
{
    Stop,
    Patrol,
    Chase,
    Wait,
    Alert,
    Search,
    Investigate,
    Defeat
}

public abstract class BaseState : IAgentState, IAgentStateMainInvokeExt
{
    protected AgentController _agentController;
    protected AgentBehaviour _agentBehaviour;

    protected AgentAnimationController _animationController;
    protected AgentEffectController _effectController;

    //protected AgentTransitions _agentTransitions;
    public BaseState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController)
    {
        _agentController = agentController;
        _agentBehaviour = behaviour;
        _animationController = animationController;
        _effectController = effectController;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void StateUpdate();

    public abstract void StateInvokeUpdate();
    /* public IAgentState GetNextState(AgentAlphabet key)
     {
         return _agentTransitions.GetNextState(key);
     }*/
}

public class PatrolState : BaseState
{
    public PatrolState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController) : base(behaviour,
        agentController, animationController, effectController)
    {
    }

    public override void Enter()
    {
        _agentController.SetNextDestination();
        _agentController.ChangeSpeed(_agentBehaviour.PatrolSpeed);
        _animationController.Patrol();
        _effectController.ColorPatrol();
    }

    public override void Exit()
    {
    }

    public override void StateUpdate()
    {
        if (_agentController.HasReached())
        {
            _agentBehaviour.AgentReachedPoint();
        }
    }

    public override void StateInvokeUpdate()
    {
    }
}

public class WaitState : BaseState
{
    public WaitState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController) : base(behaviour,
        agentController, animationController, effectController)
    {
    }

    public override void Enter()
    {

        _agentBehaviour.RotateLook(_agentController.CurrentWaypoint.transform.forward);
        _agentBehaviour.PointWait((_agentController.CurrentWaypoint as TimedWaypointController).WaitTime);
        _animationController.Idle();
        
    }

    public override void Exit()
    {

        _agentBehaviour.StopAllCoroutines();
    }

    public override void StateUpdate()
    {
    }

    public override void StateInvokeUpdate()
    {
    }
}

public class AlertState : BaseState
{
    public AlertState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController) : base(behaviour,
        agentController, animationController, effectController)
    {
    }

    public override void Enter()
    {
        _agentController.StopAgent();
        _agentBehaviour.RotateLook(_agentBehaviour.Target.position - _agentBehaviour.transform.position);
        _agentBehaviour.AlertWait();
        _animationController.Alert();
        _effectController.ColorAlert();
        _effectController.AlertEffect();
    }

    public override void Exit()
    {

        _agentBehaviour.StopAllCoroutines();
    }

    public override void StateUpdate()
    {
    }

    public override void StateInvokeUpdate()
    {
    }
}

public class ChaseState : BaseState
{
    public ChaseState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController) : base(behaviour,
        agentController, animationController, effectController)
    {
    }

    public override void Enter()
    {
        _agentController.ChangeSpeed(_agentBehaviour.ChaseSpeed);
        _animationController.Chase();
        _effectController.ColorChase();
        _effectController.ChaseEffect();
    }

    public override void Exit()
    {
    }

    public override void StateUpdate()
    {
    }

    public override void StateInvokeUpdate()
    {
        Transform target = _agentBehaviour.CheckTarget();
        if (target)
        {
            _agentController.SetTargetDestination(target.position);
        }
        else if (_agentController.HasReached())
        {
            _agentBehaviour.TargetLost();
        }
    }
}

public class SearchState : BaseState
{
    public SearchState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController) : base(behaviour,
        agentController, animationController, effectController)
    {
    }

    public override void Enter()
    {

        _agentBehaviour.SearchWait();
        _animationController.Search();
        _effectController.ColorAlert();
        _effectController.AlertEffect();
    }

    public override void Exit()
    {

        _agentBehaviour.StopAllCoroutines();
    }

    public override void StateUpdate()
    {
    }

    public override void StateInvokeUpdate()
    {
    }
}

public class InvestigateState : BaseState
{
    public InvestigateState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController) : base(behaviour,
        agentController, animationController, effectController)
    {
    }

    public override void Enter()
    {
        var position = _agentBehaviour.Target.position;
        _agentController.SetTargetDestination(
            position - (position - _agentBehaviour.transform.position).normalized * 2f);
        _agentController.ChangeSpeed(_agentBehaviour.InvestigateSpeed);
        _animationController.Patrol();
        _effectController.ColorAlert();
    }

    public override void Exit()
    {
    }

    public override void StateUpdate()
    {
        if (_agentController.HasReached())
        {
            _agentBehaviour.TargetLost();
        }
    }

    public override void StateInvokeUpdate()
    {
    }
}

public class InvestigateSearchState : BaseState
{
    public InvestigateSearchState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController) : base(behaviour,
        agentController, animationController, effectController)
    {
    }

    public override void Enter()
    {
        _agentBehaviour.RotateLook(_agentBehaviour.Target.position - _agentBehaviour.transform.position);
        _agentBehaviour.SearchInvestigateWait();
        _animationController.Search();
    }

    public override void Exit()
    {
        _agentBehaviour.StopAllCoroutines();
    }

    public override void StateUpdate()
    {
    }

    public override void StateInvokeUpdate()
    {
    }
}

public class DefeatState : BaseState
{
    public DefeatState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController) : base(behaviour,
        agentController, animationController, effectController)
    {
    }

    public override void Enter()
    {
        _agentController.StopAgent();
        if (Random.value <= 0.01f)
        {
            _animationController.Dance();
        }
        else
        {
            _animationController.Defeat();
        }
    }

    public override void Exit()
    {        
        Debug.LogWarning("Exiting end state");

        return;//not supposed to happen
    }

    public override void StateUpdate()
    {
    }

    public override void StateInvokeUpdate()
    {
    }
}

public class HaltState : BaseState
{
    public HaltState(AgentBehaviour behaviour, AgentController agentController,
        AgentAnimationController animationController, AgentEffectController effectController) : base(behaviour,
        agentController, animationController, effectController)
    {
    }

    public override void Enter()
    {
        if (_agentBehaviour.Target)
            _agentBehaviour.RotateLook(_agentBehaviour.Target.position - _agentBehaviour.transform.position);
        _agentController.StopAgent();
        if (_agentBehaviour.Intercepted)
        {
            _animationController.Kick();
        }
        else if (Random.value <= 0.01f)
        {
            _animationController.Dance();
        }
        else
        {
            _animationController.Idle();
        }
    }

    public override void Exit()
    {
        Debug.LogWarning("Exiting end state");
       return;//not supposed to happen
    }

    public override void StateUpdate()
    {
        // throw new System.NotImplementedException();
    }

    public override void StateInvokeUpdate()
    {
        //throw new System.NotImplementedException();
    }
}