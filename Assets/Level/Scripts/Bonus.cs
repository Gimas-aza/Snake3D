using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bonus : MonoBehaviour
{
    [SerializeField] private GameObject _effectEating;
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
            CreateEffect(_effectEating);

            Destroy(gameObject);
        }
    }

    private void CreateEffect(GameObject effect)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
