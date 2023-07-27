using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Spawn> _spawns;
    [SerializeField] private Snake _snake;
    [SerializeField] private Transform _containerEnemys;

    private List<Enemy> _enemies;

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
            Enemy enemy = Instantiate(spawn.EnemyPrefab, spawn.SpawnPointEnemy.position, Quaternion.identity, _containerEnemys);
            enemy.Init(_snake);
            // _enemies.Add(enemy);
        }
        spawn.TriggerSnake.Included = false;
        spawn.TriggerSnake.gameObject.SetActive(false);
    }
}
