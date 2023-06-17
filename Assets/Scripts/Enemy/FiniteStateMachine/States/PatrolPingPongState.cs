using System;
using System.Linq;
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
    private int[] _noCollisionLayers = {8, 6, 0};
    public PatrolPingPongState() : base("PatrolPingPong") { }

    protected override void OnEnterState(FiniteStateMachine fsm)
    {
        SetMovementSpeed(fsm.Config.speed);
        _initPos = fsm.enemy.position;
        _endPos = _initPos + new Vector3(fsm.Config.pingPongDistance, 0, 0);
        _turnTimer = fsm.Config.pingPongTime;
        moveDirection = fsm.Config.moveDirection;
        fsm.BoolAnimation("isTurningChase", false);
        _turn = false;
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
            fsm.Anim.SetBool("isTurningPP", _turn);
        }
        else if (!_noCollisionLayers.Contains(fsm.CurrentLayerCollision))
        {
            moveDirection *= -1;
            fsm.rb.velocity = new Vector2(0, fsm.rb.velocity.y);
            _initPos = fsm.enemy.position;
            _endPos = _initPos + new Vector3((fsm.Config.pingPongDistance+1)*moveDirection, 0, 0);
            _turn = true;
            fsm.Anim.SetBool("isTurningPP", _turn);
        }
        else if (!_turn)
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
                fsm.Anim.SetBool("isTurningPP", _turn);
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
    
    private bool CheckPositionTaken(float enemyPosX, float endPosX)
    {
        return Math.Round(enemyPosX, 1) == Math.Round(endPosX, 1);
    }
    
    
}