using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnEnemy> _spawnsEnemies;
    [SerializeField] private List<SpawnBonus> _spawnsBonus;
    [SerializeField] private SpawnSnake _spawnSnake;
    [Space(5)]
    [SerializeField] private Snake _snake;

    private List<Enemy> _enemies = new();
    private Transform _containerEnemies;
    private Transform _containerBonus;
    private Statistic _statistic;

    public UnityAction<List<Enemy>> SpawnEnemy;

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
        if (CheckTriggerForPointSpawn(0))
            SetSpawnSnake();

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
            _enemies.Add(enemy);
        }
        spawn.TriggerSnake.Included = false;
        spawn.TriggerSnake.gameObject.SetActive(false);
        SpawnEnemy?.Invoke(_enemies);
    }

    private void Spawn(SpawnBonus spawn)
    {
        if (!spawn.Prefab.TryGetComponent(out Bonus prefab)) return;

        Bonus bonus = Instantiate(prefab, spawn.SpawnPoint.position, Quaternion.identity, _containerBonus);
        bonus.Init(_snake, _statistic);
        _statistic.AddApple();
    }

    private bool CheckTriggerForPointSpawn(int index)
    {
        if (index == _spawnSnake.TriggerForPointsSpawns.Count) return false;

        return _spawnSnake.TriggerForPointsSpawns[index] != null && _spawnSnake.TriggerForPointsSpawns[index].Included;
    }

    private void SetSpawnSnake()
    {
        _spawnSnake.SpawnPoint.position = _spawnSnake.TriggerForPointsSpawns[0].transform.position;
        _spawnSnake.SpawnPoint.rotation = _spawnSnake.TriggerForPointsSpawns[0].transform.rotation;

        _spawnSnake.TriggerForPointsSpawns[0].gameObject.SetActive(false);
        _spawnSnake.TriggerForPointsSpawns.RemoveAt(0);
    }

    public void SpawnSnake()
    {
        if (!_spawnSnake.Prefab.TryGetComponent(out Snake prefab)) return;

        prefab.transform.position = _spawnSnake.SpawnPoint.position;
        prefab.transform.rotation = _spawnSnake.SpawnPoint.rotation;
    }
}
