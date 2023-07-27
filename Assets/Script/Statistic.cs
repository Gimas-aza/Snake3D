using UnityEngine;

public class Statistic
{
    private int _numberOfEnemy;
    private int _numberDieOfEnemy;
    private int _numberEatOfApple;

    public int NumberOfEnemy => _numberOfEnemy;
    public int NumberDieOfEnemy => _numberDieOfEnemy;
    public int NumberEatOfApple => _numberEatOfApple;

    public void AddEnemy()
    {
        _numberOfEnemy++;
        Debug.Log(_numberOfEnemy);
    }

    public void AddDieOfEnemy()
    {
        _numberDieOfEnemy++;
    }

    public void AddEatOfApple()
    {
        _numberEatOfApple++;
    }
}
