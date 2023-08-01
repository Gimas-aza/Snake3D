using UnityEngine;
using Zenject;

public class Snake : Character
{
    [SerializeField] private GameObject _effect;
    [Space(5)]
    [SerializeField] private int _healthMax;

    private int _health;
    private UI _ui;

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
            gameObject.SetActive(false);
        }

        _ui.SetHealthBar(_health);
    }

    public void RecoverHealth()
    {
        _health = _healthMax;
        _ui.SetHealthBar(_health);
    } 
}
