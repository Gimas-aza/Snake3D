using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EndOfLevel : MonoBehaviour
{
    private UI _ui;

    [Inject]
    private void Construct(UI ui)
    {
        _ui = ui;
    }

    private void Start()
    {
        _ui.SetActiveStatistics(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Snake snake))
        {
            _ui.SetActiveStatistics(true);
            Time.timeScale = 0;
        }
    }
}
