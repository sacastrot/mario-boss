public class Enemy : LivingEntity
{
    private FiniteStateMachine _fsm;
    private EnemyConfig _config;
    public EnemyConfig Config => _config;
    void Start()
    {
        InitHealth();

        _config = GetComponent<EnemyConfig>();
        _fsm = GetComponent<FiniteStateMachine>();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        _fsm.ToState(StateType.Dead);
        //TODO: Communicate Enemy death
    }
}