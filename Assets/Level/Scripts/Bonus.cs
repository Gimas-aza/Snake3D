using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bonus : MonoBehaviour
{
    private Snake _snake;
    private Statistic _statistic;

    public void Init(Snake snake, Statistic statistic)
    {
        _snake = snake;
        _statistic = statistic;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SnakeTail snakeTail))
        {
            snakeTail.AddBlock();
            _snake.RecoverHealth();
            _statistic.AddEatenApple();

            Destroy(gameObject);
        }
    }
}
