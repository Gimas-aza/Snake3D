using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _startPositionHead = Vector3.zero;

    public void Rotation(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion look = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_head.rotation, look, _rotationSpeed * Time.deltaTime).eulerAngles;       
        _head.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
    }

    public void RotationForward()
    {
        Vector3 rotation = Vector3.Lerp(_head.localEulerAngles, _startPositionHead, (_rotationSpeed - 2) * Time.deltaTime);
        _head.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0);
    }
}
