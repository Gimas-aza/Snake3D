using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Head))]
[RequireComponent(typeof(Attack))]
public class Turret : MonoBehaviour
{
    [SerializeField] private TurretBullet _bullet;
    [Space(5)]
    [SerializeField] private float _numderOfBullet;
    [SerializeField] private float _range;

    private List<TurretBullet> _bulletList = new();
    private Transform _containerBullet;
    private float _shortestDistance;
    private List<Enemy> _enemies = new();
    private Enemy _nearestEnemy;
    private Head _head;
    private Attack _attack;

    public void Init(Transform containerBullet)
    {
        _containerBullet = containerBullet;
        _head = GetComponent<Head>();
        _attack = GetComponent<Attack>();
    }

    private void Start()
    {
        for (int i = 0; i < _numderOfBullet; i++)
        {
            TurretBullet bullet = Instantiate(_bullet, _containerBullet);
            bullet.gameObject.SetActive(false);
            _bulletList.Add(bullet);
        }
    }

    private void Update()
    {
        FindNearestEnemy();

        if(_nearestEnemy != null && _shortestDistance <= _range)
        {
            _head.Rotation(_nearestEnemy.transform);
            _attack.Shot(_nearestEnemy.transform, _bulletList);
        }
        else
        {
            _head.RotationForward();
        }
    }

    public void FindEnemies()
    {
        _enemies = FindObjectsOfType<Enemy>().ToList();
    }

    private void FindNearestEnemy()
    {
        FindEnemies();
        _shortestDistance = Mathf.Infinity;

        foreach(Enemy enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < _shortestDistance)
            {
                _shortestDistance = distance;
                _nearestEnemy = enemy;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
