using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour {
    [SerializeField] private float MoveSpeed = 5;
    [SerializeField] private float SteerSpeed = 180;

    void Update() {

        MoveForward();
        SetTurnMove();
    }

    void MoveForward() 
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }

    void SetTurnMove() 
    {
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);
    }
}