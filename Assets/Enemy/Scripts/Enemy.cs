using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Snake _target;
    [SerializeField] private GameObject _effect;
    [Space(5)]
    [SerializeField] private int _health = 10;
    [SerializeField] private TextMesh _healthText;
    [Space(5)]

    private NavMeshAgent _agent;

    public Snake Target => _target;

    private void Start()
    {
        // _healthText.text = _health.ToString();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            // Instantiate(_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // _healthText.text = _health.ToString();
    }
}
