using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 positionToGo = _target.transform.position + _startPosition;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, positionToGo, 0.125f);

        transform.position = smoothPosition;
        transform.LookAt(_target.transform.position);
    }
}
