using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnEnemy> _spawnsEnemies;
    [SerializeField] private List<SpawnBonus> _spawnsBonus;
    [SerializeField] private Snake _snake;

    private Transform _containerEnemies;
    private Transform _containerBonus;
    private Statistic _statistic;

    [Inject]
    private void Construct(Statistic statistic, Containers containers)
    {
        _statistic = statistic;
        _containerEnemies = containers.ContainerEnemy.transform;
        _containerBonus = containers.ContainerBonus.transform;
    }

    private void Start()
    {
        foreach (var spawn in _spawnsBonus)
        {
            Spawn(spawn);
        }

        foreach (var spawn in _spawnsEnemies)
        {
            _statistic.AddEnemy(spawn.Number);
        }
    }

    private void Update()
    {
        if (_spawnsEnemies.Count == 0)
            return;

        foreach (var spawn in _spawnsEnemies)
        {
            if (spawn.TriggerSnake.Included)
                Spawn(spawn);
        }
    }

    private void Spawn(SpawnEnemy spawn)
    {
        for (int i = 0; i < spawn.Number; i++)
        {
            if (!spawn.Prefab.TryGetComponent(out Enemy prefab)) continue;

            Enemy enemy = Instantiate(prefab, spawn.SpawnPoint.position, Quaternion.identity, _containerEnemies);
            enemy.Init(_snake, _statistic);
        }
        spawn.TriggerSnake.Included = false;
        spawn.TriggerSnake.gameObject.SetActive(false);
    }

    private void Spawn(SpawnBonus spawn)
    {
        if (!spawn.Prefab.TryGetComponent(out Bonus prefab)) return;

        Bonus bonus = Instantiate(prefab, spawn.SpawnPoint.position, Quaternion.identity, _containerBonus);
        bonus.Init(_snake, _statistic);
        _statistic.AddApple();
    }
}
