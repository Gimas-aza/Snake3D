using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SpawnTrigger : MonoBehaviour
{
    public bool Included = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Snake player))
        {
            Included = true;
        }
    }
}
