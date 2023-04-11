using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly WaypointController[] _waypoints;

    private WaypointController _currentWaypoint;

    public WaypointController CurrentWaypoint => _currentWaypoint;

    private int _waypointNum = -1;

    private readonly float _distanceTolerance;

    public AgentController(NavMeshAgent navMeshAgent, WaypointController[] waypoints, float distanceTolerance)
    {
        _navMeshAgent = navMeshAgent;
        _waypoints = waypoints;
        _distanceTolerance = distanceTolerance;
    }

    public void InitAgent()
    {
        _navMeshAgent.enabled = true;
    }

    public void SetNextDestination()
    {
        IterateDestination();
        _navMeshAgent.SetDestination(_currentWaypoint.Point);
    }

    public void SetTargetDestination(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    public void ChangeSpeed(float speed)
    {
        _navMeshAgent.speed = speed;
    }
    public void StopAgent()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
    }

    public bool HasReached()
    {
        return !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < _distanceTolerance &&
               (!_navMeshAgent.hasPath ||
                _navMeshAgent.velocity.sqrMagnitude == 0);
    }

    private void IterateDestination()
    {
        _waypointNum++;
        if (_waypointNum == _waypoints.Length)
        {
            _waypointNum = 0;
        }

        _currentWaypoint = _waypoints[_waypointNum];
    }
}