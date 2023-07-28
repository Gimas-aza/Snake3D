using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Spawn> _spawns;
    [SerializeField] private Snake _snake;

    private Transform _containerEnemies;
    private Statistic _statistic;

    [Inject]
    private void Construct(Statistic statistic, Containers containers)
    {
        _statistic = statistic;
        _containerEnemies = containers.ContainerEnemy.transform;
    }

    private void Update()
    {
        if (_spawns.Count == 0)
            return;

        foreach (var spawn in _spawns)
        {
            if (spawn.TriggerSnake.Included)
                Spawn(spawn);
        }
    }

    private void Spawn(Spawn spawn)
    {
        for (int i = 0; i < spawn.NumberEnemy; i++)
        {
            Enemy enemy = Instantiate(spawn.EnemyPrefab, spawn.SpawnPointEnemy.position, Quaternion.identity, _containerEnemies);
            enemy.Init(_snake);
            _statistic.AddEnemy();
        }
        spawn.TriggerSnake.Included = false;
        spawn.TriggerSnake.gameObject.SetActive(false);
    }
}
