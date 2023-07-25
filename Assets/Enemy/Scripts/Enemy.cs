using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [Space(5)]
    [SerializeField] private int _health = 10;
    [SerializeField] private TextMesh _healthText;
    [Space(5)]

    private Snake _target;

    public Snake Target => _target;

    public void Init(Snake target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            // Instantiate(_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
