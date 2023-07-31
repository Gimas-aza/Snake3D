using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [Space(5)]
    [SerializeField] private int _health;
    [SerializeField] private TextMesh _healthText;
    [Space(5)]

    private Snake _target;
    private Statistic _statistic;

    public Snake Target => _target;
    public Statistic Statistic => _statistic;

    public void Init(Snake target, Statistic statistic)
    {
        _target = target;
        _statistic = statistic;
    }

    public void TakeDamage(int damage)
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
