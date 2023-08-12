using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EndOfLevel : MonoBehaviour
{
    [SerializeField] private AudioSource _gameOverEffect;
    [SerializeField] private AudioSource _successEffect;
    [SerializeField] private AudioSource _music;

    private UI _ui;
    private Snake _snake;

    [Inject]
    private void Construct(UI ui, Snake snake)
    {
        _ui = ui;
        _snake = snake;
    }

    private void Start()
    {
        _ui.SetActiveStatistics(false);
        _snake.Death += OnDeathSnake;
    }

    private void OnDisable()
    {
        _snake.Death -= OnDeathSnake;
    }

    private void OnDeathSnake()
    {
        _music?.Stop();
        _gameOverEffect?.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Snake snake))
        {
            _ui.SetLevelPassed(true);
            _ui.SetActiveStatistics(true);
            _music?.Stop();
            _successEffect?.Play();
            Time.timeScale = 0;
        }
    }
}
