using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private GameObject _effect;
    [Space(5)]
    [SerializeField] private int _health;
    [SerializeField] private TextMesh _healthText;
    [Space(5)]

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
            // Instantiate(_effect, transform.position, Quaternion.identity);
            _statistic.AddDiedEnemy();
            Destroy(gameObject);
        }
    }
}
