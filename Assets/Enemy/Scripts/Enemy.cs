using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int _health;
    
    private StateMachine _stateMachine;
    private Statistic _statistic;

    public Statistic Statistic => _statistic;

    public Transform PivotModel;

    public void Init(Snake target, Statistic statistic)
    {
        _statistic = statistic;
        _stateMachine = GetComponent<StateMachine>();
        _stateMachine.Init(target);
    }

    public override void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            _statistic.AddDiedEnemy();
            Destroy(gameObject);
        }
    }
}
