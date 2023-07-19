using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    [SerializeField] private float _bodySpeed = 5;
    [SerializeField] private int _gap;
    [SerializeField] private GameObject _bodyPrefab;
    [SerializeField] private Transform _spawnBody;

    private List<GameObject> _bodyParts = new List<GameObject>();
    private List<Vector3> _positionsHistory = new List<Vector3>();

    private void Start()
    {
        AddBlock();
        AddBlock();
        AddBlock();
        AddBlock();
    }

    private void Update()
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
            body.transform.position += moveDirection * _bodySpeed * Time.deltaTime;

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
        GameObject body = Instantiate(_bodyPrefab);
        _bodyParts.Add(body);
    }

    public void RemoveBlock()
    {
        Destroy(_bodyParts[0].gameObject);
        _bodyParts.RemoveAt(0);
    }
}
