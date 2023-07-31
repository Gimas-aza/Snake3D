using UnityEngine;
using Zenject;

public class Statistic
{
    private int _numberEnemies;
    private int _numberDiedEnemies;
    private int _numberApples;
    private int _numberEatenApples;

    public int NumberEnemies => _numberEnemies;
    public int NumberDiedEnemies => _numberDiedEnemies;
    public int NumberEatenApples => _numberEatenApples;
    public int NumberApples => _numberApples;

    private UI _ui;

    [Inject]
    private void Construct(UI ui)
    {
        _ui = ui;
    }

    public void AddEnemy(int numberEnemies)
    {
        _numberEnemies += numberEnemies;
        _ui.SetStatisticEnemies(_numberDiedEnemies, _numberEnemies);
    }

    public void AddDiedEnemy()
    {
        _numberDiedEnemies++;
        _ui.SetStatisticEnemies(_numberDiedEnemies, _numberEnemies);
    }

    public void AddApple()
    {
        _numberApples++;
        _ui.SetStatisticApples(_numberEatenApples, _numberApples);
    }

    public void AddEatenApple()
    {
        _numberEatenApples++;
        _ui.SetStatisticApples(_numberEatenApples, _numberApples);
    }
}
