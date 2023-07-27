using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Snake _snake;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SnakeTail snake))
        {
            _snake = snake.GetComponent<Snake>();

            snake.AddBlock();
            _snake.RecoverHealth();

            Destroy(gameObject);
        }
    }
}
