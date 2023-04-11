using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentBehaviour : MonoBehaviour
{
    [SerializeField] private float invokeTime;

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private WaypointController[] waypoints;
    [SerializeField] private float distanceTolerance;

    [SerializeField] private GuardDetection detection;
    [SerializeField] private FootprintDetection footprintDetection;
    [SerializeField] private AgentHurtbox hurtbox;
    [SerializeField] private AgentHitbox hitbox;
    [SerializeField] private float alertWait;
    [SerializeField] private float searchTime;
    [SerializeField] private float searchInvestigateTime;
    private Transform _target;
    public Transform Target => _target;

    public float PatrolSpeed => patrolSpeed;

    public float InvestigateSpeed => investigateSpeed;

    public float ChaseSpeed => chaseSpeed;

    public bool Intercepted { get; private set; } = false;

    [SerializeField] private float rotationTime;
    [SerializeField] private float rotationYOffset = 2f;
    private bool _playerSighted;

    private AgentController _agentController;
    private AgentAnimationController _animationController;
    private AgentStateMachine _stateMachine;
    private PatrolState _patrolState;
    private WaitState _waitState;
    private AlertState _alertState;
    private ChaseState _chaseState;
    private SearchState _searchState;
    private InvestigateState _investigateState;
    private InvestigateSearchState _investigateSearchState;
    private DefeatState _defeatState;
    private HaltState _haltState;

    [SerializeField] private float patrolSpeed;
    [SerializeField] private float investigateSpeed;
    [SerializeField] private float chaseSpeed;

    [SerializeField] private Animator animator;

    [SerializeField] private AgentEffectController effectController;

    private void Awake()
    {
        detection.TargetSighted += DetectionOnTargetSighted;
        hurtbox.NoiseHeard += HurtboxOnNoiseHeard;
        hitbox.TargetIntercepted += HitboxOnTargetIntercepted;
        footprintDetection.FootprintSpotted += FootprintDetectionOnFootprintSpotted;
        _agentController = new AgentController(navMeshAgent, waypoints, distanceTolerance);
        _animationController = new AgentAnimationController(animator);
        _patrolState = new PatrolState(this, _agentController, _animationController, effectController);
        _waitState = new WaitState(this, _agentController, _animationController, effectController);
        _alertState = new AlertState(this, _agentController, _animationController, effectController);
        _chaseState = new ChaseState(this, _agentController, _animationController, effectController);
        _searchState = new SearchState(this, _agentController, _animationController, effectController);
        _investigateState = new InvestigateState(this, _agentController, _animationController, effectController);
        _investigateSearchState =
            new InvestigateSearchState(this, _agentController, _animationController, effectController);
        _defeatState = new DefeatState(this, _agentController, _animationController, effectController);
        _haltState = new HaltState(this, _agentController, _animationController, effectController);
        AgentTransitions patrolTransitions = new AgentTransitions(new Dictionary<AgentAlphabet, BaseState>
        {
            {AgentAlphabet.Patrol, _patrolState},
            {AgentAlphabet.Wait, _waitState},
            {AgentAlphabet.Alert, _alertState},
            {AgentAlphabet.Stop, _haltState},
            {AgentAlphabet.Defeat, _defeatState}
        });
        AgentTransitions waitTransitions = new AgentTransitions(new Dictionary<AgentAlphabet, BaseState>
        {
            {AgentAlphabet.Patrol, _patrolState},
            {AgentAlphabet.Alert, _alertState},
            {AgentAlphabet.Stop, _haltState},
            {AgentAlphabet.Defeat, _defeatState}
        });
        AgentTransitions alertTransitions = new AgentTransitions(new Dictionary<AgentAlphabet, BaseState>
        {
            {AgentAlphabet.Chase, _chaseState},
            {AgentAlphabet.Alert, _alertState},
            {AgentAlphabet.Investigate, _investigateState},
            {AgentAlphabet.Stop, _haltState},
            {AgentAlphabet.Defeat, _defeatState}
        });
        AgentTransitions chaseTransitions = new AgentTransitions(new Dictionary<AgentAlphabet, BaseState>
        {
            {AgentAlphabet.Search, _searchState},
            {AgentAlphabet.Alert, _chaseState},
            {AgentAlphabet.Stop, _haltState},
            {AgentAlphabet.Defeat, _defeatState}
        });
        AgentTransitions searchTransitions = new AgentTransitions(new Dictionary<AgentAlphabet, BaseState>
        {
            {AgentAlphabet.Alert, _alertState},
            {AgentAlphabet.Patrol, _patrolState},
            {AgentAlphabet.Investigate, _investigateState},
            {AgentAlphabet.Stop, _haltState},
            {AgentAlphabet.Defeat, _defeatState}
        });
        AgentTransitions investigateTransitions = new AgentTransitions(new Dictionary<AgentAlphabet, BaseState>
        {
            {AgentAlphabet.Alert, _alertState},
            {AgentAlphabet.Search, _investigateSearchState},
            {AgentAlphabet.Stop, _haltState},
            {AgentAlphabet.Defeat, _defeatState}
        });
        AgentTransitions investigateSearchTransitions = new AgentTransitions(new Dictionary<AgentAlphabet, BaseState>
        {
            {AgentAlphabet.Alert, _alertState},
            {AgentAlphabet.Patrol, _patrolState},
            {AgentAlphabet.Investigate, _investigateState},
            {AgentAlphabet.Stop, _haltState},
            {AgentAlphabet.Defeat, _defeatState}
        });
        AgentTransitions defeatTransitions = new AgentTransitions(new Dictionary<AgentAlphabet, BaseState>());
        AgentTransitions haltTransitions = new AgentTransitions(new Dictionary<AgentAlphabet, BaseState>());
        _stateMachine = new AgentStateMachine(new Dictionary<BaseState, AgentTransitions>
            {
                {_patrolState, patrolTransitions},
                {_waitState, waitTransitions},
                {_alertState, alertTransitions},
                {_chaseState, chaseTransitions},
                {_searchState, searchTransitions},
                {_investigateState, investigateTransitions},
                {_investigateSearchState, investigateSearchTransitions},
                {_defeatState, defeatTransitions},
                {_haltState, haltTransitions}
            }
            , _patrolState);
        //_stateMachine.StartMachine();

        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void FootprintDetectionOnFootprintSpotted(Transform obj)
    {
        _target = obj;
        _stateMachine.SwitchState(AgentAlphabet.Alert);

        footprintDetection.ToggleTrigger(false);
    }

    private void HitboxOnTargetIntercepted(Transform obj)
    {
        _target = obj;
        detection.ToggleTrigger(false);
        footprintDetection.ToggleTrigger(false);
        Intercepted = true;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        if (obj.IsWin)
            _stateMachine.SwitchState(AgentAlphabet.Defeat);
        else
            _stateMachine.SwitchState(AgentAlphabet.Stop);
    }

    private void HurtboxOnNoiseHeard(Transform obj)
    {
        _target = obj;
        _stateMachine.SwitchState(AgentAlphabet.Alert);
    }

    private void Start()
    {
        _agentController.InitAgent();
        _stateMachine.StartMachine();
        InvokeRepeating(nameof(MainCalculationUpdate), 0, invokeTime);
    }

    private void DetectionOnTargetSighted(Transform obj)
    {
        _target = obj;
        detection.ToggleTrigger(false);
        hurtbox.ToggleCollider(false);
        footprintDetection.ToggleTrigger(false);

        _playerSighted = true;
        _stateMachine.SwitchState(AgentAlphabet.Alert);
    }

    private void Update()
    {
        _stateMachine.CurrentState.StateUpdate();
    }

    private void MainCalculationUpdate()
    {
        _stateMachine.CurrentState.StateInvokeUpdate();
    }

    public void AgentReachedPoint()
    {
        if (_agentController.CurrentWaypoint is TimedWaypointController)
        {
            _stateMachine.SwitchState(AgentAlphabet.Wait);
        }
        else
        {
            _stateMachine.SwitchState(AgentAlphabet.Patrol);
        }
    }

    public Transform CheckTarget()
    {
        return detection.CheckVisualContact(_target);
    }

    public void TargetLost()
    {
        _stateMachine.SwitchState(AgentAlphabet.Search);
        _playerSighted = false;
        detection.ToggleTrigger(true);
        footprintDetection.ToggleTrigger(true);
        hurtbox.ToggleCollider(true);
    }

    public void PointWait(float time)
    {
        StartCoroutine(WaitCoroutine(time, AgentAlphabet.Patrol));
    }

    public void AlertWait()
    {
        if (_playerSighted)
            StartCoroutine(WaitCoroutine(alertWait, AgentAlphabet.Chase));
        else
            StartCoroutine(WaitCoroutine(alertWait, AgentAlphabet.Investigate));
    }

    public void SearchWait()
    {
        StartCoroutine(WaitCoroutine(searchTime, AgentAlphabet.Patrol));
    }

    public void SearchInvestigateWait()
    {
        StartCoroutine(WaitCoroutine(searchInvestigateTime, AgentAlphabet.Patrol));
    }

    public void RotateLook(Vector3 lookVector)
    {
        lookVector.y = rotationYOffset;
        StartCoroutine(RotateCoroutine(rotationTime, Quaternion.LookRotation(lookVector)));
    }


    private IEnumerator RotateCoroutine(float time, Quaternion targetRotation)
    {
        Quaternion startRotation = transform.rotation;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t / time);
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    private IEnumerator WaitCoroutine(float time, AgentAlphabet switchKey)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }

        _stateMachine.SwitchState(switchKey);
    }
}