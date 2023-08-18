using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class SnakeMove : MonoBehaviour {
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _steerSpeed;
    [SerializeField] private float _smoothClick;

    private UI _ui;

    [Inject]
    private void Construct(UI ui)
    {
        _ui = ui;
    }

    private void Start()
    {
    }

    private void FixedUpdate() {

        MoveForward();
        SetTurnMove();
    }

    private void OnButtonUp(PointerUpEvent evt)
    {
        Debug.Log("ButtonUp");
    }

    private void MoveForward() 
    {
        transform.position += transform.forward * _moveSpeed * Time.fixedDeltaTime;
    }

    private void SetTurnMove() 
    {
        float steerDirection = 0;

        if (_ui.Horizontal == 0)
            steerDirection = Input.GetAxis("Horizontal");
        else
            steerDirection = _ui.Horizontal;

        transform.Rotate(Vector3.up * steerDirection * _steerSpeed * Time.fixedDeltaTime);
    }
}