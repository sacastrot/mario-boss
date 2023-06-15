using System;
using UnityEngine;

public class PatrolPingPongState: State
{
    public override StateType Type { get; }
    public float currentSpeed;
    public int moveDirection;
    private Vector3 _initPos;
    private Vector3 _endPos;
    private bool _turn;
    private float _turnTimer; //EnemyConfig
    public PatrolPingPongState() : base("PatrolPingPong") { }

    protected override void OnEnterState(FiniteStateMachine fsm)
    {
        SetMovementSpeed(fsm.Config.speed);
        _initPos = fsm.enemy.position;
        _endPos = _initPos + new Vector3(fsm.Config.pingPongDistance, 0, 0);
        _turnTimer = fsm.Config.pingPongTime;
        moveDirection = fsm.Config.moveDirection;
    }

    protected override void OnUpdateState(FiniteStateMachine fsm, float deltaTime)
    {
        if (CheckPositionTaken(fsm.enemy.position.x, _endPos.x))
        {
            moveDirection *= -1;
            fsm.rb.velocity = new Vector2(0, fsm.rb.velocity.y);
            _initPos = fsm.enemy.position;
            _endPos = _initPos + new Vector3(fsm.Config.pingPongDistance*moveDirection, 0, 0);
            _turn = true;
            fsm.TriggerAnimation("isTurningPP");
        }
        if (!_turn)
        {
            fsm.enemy.localScale = new Vector2(moveDirection, 1);
            fsm.rb.velocity = new Vector2(moveDirection * currentSpeed, fsm.rb.velocity.y);
            fsm.TriggerAnimation("isWalking");
        }
        else
        {
            _turnTimer -= Time.deltaTime;
            if (_turnTimer <= 0)
            {
                _turn = false;
                _turnTimer = fsm.Config.pingPongTime;
            }
        }
    }

    protected override void OnExitState(FiniteStateMachine fsm)
    {
    }
    
    private void SetMovementSpeed(float configSpeed)
    {
        currentSpeed = configSpeed;
    }
    
    
    // private void Move(Transform rb)
    // {
    //     float d = (_initPos - _endPos).magnitude;
    //     float delta = Mathf.pingPong(Time.time * currentSpeed, d*1.3f);
    //     enemy.position = Vector3.Lerp(_initPos, _endPos, (delta / d));
    // }

    private bool CheckPositionTaken(float enemyPosX, float endPosX)
    {
        return Math.Round(enemyPosX, 1) == Math.Round(endPosX, 1);
    }
    
    //To support external coroutines being called:
    // public void StartChildCoroutine(IEnumerator coroutineMethod)
    // {
    //     StartCoroutine(coroutineMethod);
    // }
    //
    // public void MapTransitionDelayed()
    // {
    //    StartChildCoroutine(DelayedMapTransition());
    // }
    
}