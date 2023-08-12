using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Snake : Character
{
    [Space(5)]
    [SerializeField] private int _healthMax;

    private int _health;
    private UI _ui;

    public UnityAction Death;

    [Inject]
    private void Construct(UI ui)
    {
        _ui = ui;
    }

    private void Start()
    {
        _health = _healthMax;
        _ui.SetHealthBar(_health);
    }

    public override void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            // Instantiate(_effect, transform.position, Quaternion.identity);
            _health = 0;
            Death?.Invoke();
            gameObject.SetActive(false);
            SetGameOver();
        }

        _ui.SetHealthBar(_health);
    }

    public void RecoverHealth()
    {
        _health = _healthMax;
        _ui.SetHealthBar(_health);
    }

    private void SetGameOver()
    {
        _ui.SetLevelPassed(false);
        _ui.SetActiveStatistics(true);
        Time.timeScale = 0;
    }
}
