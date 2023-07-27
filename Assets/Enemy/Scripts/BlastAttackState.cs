using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastAttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _delayAttack;

    private void Start()
    {
        Invoke(nameof(TryAttack), _delayAttack);  
    }

    private void TryAttack()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) <= _radius)
            Target.TakeDamage(_damage);
        
        Debug.Log("Burst");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
