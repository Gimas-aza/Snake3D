using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SnakeTail : MonoBehaviour
{
    [SerializeField] private float _bodySpeed;
    [SerializeField] private int _gap;
    [SerializeField] private SnakeBody _bodyPrefab;
    [SerializeField] private Transform _spawnBody;

    private Transform _containerBody;
    private Transform _containerBullet;
    private Spawner _spawner;
    private List<SnakeBody> _bodyParts = new();
    private List<Vector3> _positionsHistory = new();

    [Inject]
    private void Construct(Containers containers, Spawner spawner)
    {
        _containerBody = containers.ContainerSnakeTail.transform;
        _containerBullet = containers.ContainerBullet.transform;
        _spawner = spawner;
    }

    private void FixedUpdate()
    {
        MoveTail();
        LimitPositionHistory();
    }

    private void MoveTail()
    {
        _positionsHistory.Insert(0, _spawnBody.position);

        int index = 0;
        foreach (var body in _bodyParts) 
        {
            Vector3 point = _positionsHistory[Mathf.Clamp(index * _gap, 0, _positionsHistory.Count - 1)];

            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * _bodySpeed * Time.fixedDeltaTime;

            body.transform.LookAt(point);

            index++;
        }
    }

    private void LimitPositionHistory()
    {
        if (_positionsHistory.Count > _bodyParts.Count * _gap)
        {
            _positionsHistory.RemoveAt(_positionsHistory.Count - 1);
        }
    }

    public void AddBlock()
    {
        SnakeBody body = Instantiate(_bodyPrefab, _containerBody);
        body.Turret.Init(_containerBullet, _spawner);
        _bodyParts.Add(body);
    }

    public void RemoveBlock()
    {
        Destroy(_bodyParts[0].gameObject);
        _bodyParts.RemoveAt(0);
    }
}
