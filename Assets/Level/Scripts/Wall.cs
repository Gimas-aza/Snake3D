using UnityEngine;
using Zenject;

public class Wall : MonoBehaviour
{
    private int _damage = 20;
    private Spawner _spawner;

    [Inject]
    private void Construct(Spawner spawner)
    {
        _spawner = spawner;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Snake snake))
        {
            snake.TakeDamage(_damage);
            _spawner.SpawnSnake();
        }
    }
}
