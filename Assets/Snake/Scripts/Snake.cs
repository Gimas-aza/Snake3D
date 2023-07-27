using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [Space(5)]
    [SerializeField] private int _healthMax;

    private int _health;

    private void Start()
    {
        _health = _healthMax;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            // Instantiate(_effect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    public void RecoverHealth()
    {
        _health = _healthMax;
    } 
}
