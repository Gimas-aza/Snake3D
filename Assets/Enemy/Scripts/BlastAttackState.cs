using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastAttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;

    private void Start()
    {
        Attack();
    }

    private void Attack()
    {
        Debug.Log("Attack");
        // Destroy(gameObject);
    }
}
