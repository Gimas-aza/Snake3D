using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMove : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private Transform[] _firePoints;
    [SerializeField] private TurretBullet _bullet;
    [Space(5)]
    [SerializeField] private float _numderOfBullet = 50;
    [SerializeField] private Transform _containerBullet;
    [SerializeField] private float _range = 15;
    [SerializeField] private float _rotationSpeed = 3;
    [SerializeField] private float _fireRate = 1;

    private List<TurretBullet> _bulletList = new();
    private int _currentBullet = 0;
    private Vector3 _target;
    private float _shortestDistance;
    private Enemy _nearestEnemy;
    private float _countdown;

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
        SetDistanceToNearestEnemy();

        if(_nearestEnemy != null && _shortestDistance <= _range)
        {
            RotationHead();
            Shot();
        }
    }

    private void SetDistanceToNearestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); //TODO Перенести в спавнер врагов
        _shortestDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < _shortestDistance)
            {
                _shortestDistance = distance;
                _nearestEnemy = enemy;
            }
        }
    }

    private void RotationHead()
    {
        _target = _nearestEnemy.transform.position - new Vector3(0, 0.5f, 0);
        Vector3 direction = _target - new Vector3(0, 0.5f, 0);
        Quaternion look = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_head.rotation, look, _rotationSpeed * Time.deltaTime).eulerAngles;       
        _head.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
    }

    private void Shot()
    {
        if(_countdown <= 0)
        {
            if (_currentBullet == _numderOfBullet)
            {
                _currentBullet = 0;
            }

            for(int i = 0; i < _firePoints.Length; i++)
            {
                TurretBullet bullet = _bulletList[_currentBullet++];
                bullet.gameObject.SetActive(true);
                bullet.transform.position = _firePoints[i].position;
                bullet.transform.rotation = _firePoints[i].rotation;

                bullet.ShotBullet(_target);
            }

            _countdown = 1 / _fireRate;
        }

        _countdown -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
