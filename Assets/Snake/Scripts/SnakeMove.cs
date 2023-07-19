using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour {
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _steerSpeed = 180;

    void Update() {

        MoveForward();
        SetTurnMove();
    }

    void MoveForward() 
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }

    void SetTurnMove() 
    {
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * _steerSpeed * Time.deltaTime);
    }
}