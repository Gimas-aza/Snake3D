using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EndOfLevel : MonoBehaviour
{
    [SerializeField] private AudioSource _gameOverEffect;
    [SerializeField] private AudioSource _successEffect;
    [SerializeField] private AudioSource _music;
    [SerializeField] private List<GameObject> _levelBarriers;
    [SerializeField] private int _minNumberOfEnemies;

    private Vector3 _openDoorPosition = new Vector3(0, -90, 0);
    private bool _isOpenBarrier = false;
    private UI _ui;
    private Snake _snake;
    private Statistic _statistic;

    [Inject]
    private void Construct(UI ui, Snake snake, Statistic statistic)
    {
        _ui = ui;
        _snake = snake;
        _statistic = statistic;
    }

    private void Start()
    {
        _ui.SetActiveStatistics(false);
        _snake.Death += OnDeathSnake;
        _statistic.DeathEnemy += OnDeathEnemy;
    }

    private void OnDisable()
    {
        _snake.Death -= OnDeathSnake;
        _statistic.DeathEnemy -= OnDeathEnemy;
    }

    private void OnDeathSnake()
    {
        _music?.Stop();
        _gameOverEffect?.Play();
    }

    private void OnDeathEnemy()
    {
        if (_minNumberOfEnemies <= _statistic.NumberDiedEnemies && !_isOpenBarrier)
        {
            foreach (var barrier in _levelBarriers)
            {
                barrier.transform.eulerAngles = _openDoorPosition;
            }
            _isOpenBarrier = true;
        }
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
