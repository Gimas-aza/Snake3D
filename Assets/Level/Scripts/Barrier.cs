using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _pointer;

    private Vector3 _openDoorRotation = new Vector3(0, -90, 0);

    public void OpenDoor()
    {
        transform.eulerAngles = _openDoorRotation; 
    }

    public void SetActiveCollider(bool isActive)
    {
        _collider.enabled = isActive;
    }

    public void SetActivePointer(bool isActive)
    {
        _pointer.SetActive(isActive);
    }
}
