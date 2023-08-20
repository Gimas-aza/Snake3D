using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerChangeOrbit : MonoBehaviour
{
    public UnityAction ChangeOrbit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Snake snake))
        {
            ChangeOrbit?.Invoke();
        }
    }
}
