
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class TauntExitState: State
{
    public TauntExitState() : base("TauntExit") { }
    public override StateType Type { get; }
    private Rigidbody2D _rb;
    protected override void OnEnterState(FiniteStateMachine fsm)
    {
        fsm.TriggerAnimation("isJump");
        _rb = fsm.transform.GetComponent<Rigidbody2D>();
        
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
        _rb.gravityScale = 1.5f;
    }

    protected override void OnUpdateState(FiniteStateMachine fsm, float deltaTime)
    {
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}
