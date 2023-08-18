using UnityEngine;
using Zenject;

public class Wall : MonoBehaviour
{
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
            snake.TakeDamage(20);
            _spawner.SpawnSnake();
        }
    }
}
