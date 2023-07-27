using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TurretBullet : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [Space(5)]
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 50;
    [SerializeField] private float _timeLife;
    
    private Transform _target;
    private Rigidbody _rigidbody;
    private float _countTime;

    private void Awake()
    {
        _countTime = _timeLife;
    }

    private void Update() 
    {
        if (_countTime <= 0)
        {
            _rigidbody.isKinematic = true;
            gameObject.SetActive(false);
        }

        _countTime -= Time.deltaTime;
    }

    public void ShotBullet(Transform target)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _target = target;
        _countTime = _timeLife;

        Vector3 direction = _target.position - transform.position;

        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * _speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            // Instantiate(_effect, transform.position, Quaternion.identity);
        }
        _rigidbody.isKinematic = true;
        gameObject.SetActive(false);
    }
}
