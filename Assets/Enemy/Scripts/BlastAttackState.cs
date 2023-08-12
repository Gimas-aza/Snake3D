using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlastAttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _delayAttack;
    [Header("Visual effects")]
    [SerializeField] private Image _imageRadiusAttack;
    [SerializeField] private GameObject _effectBlast;
    [Header("Time animation")]
    [SerializeField] private float _waitTime;

    private Enemy _enemy;
    private Statistic _statistic;
    private Vector3 _bottomPosition = new Vector3(0, -0.34f, 0);
    private Vector2 _sizeRadiusAttack;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _statistic = _enemy.Statistic;
        _sizeRadiusAttack = new Vector2(_radius * 2, _radius * 2);

        StartCoroutine(LowerTheModel());
        StartCoroutine(DrawRadiusAttack());
        Invoke(nameof(TryAttack), _delayAttack);  
    }

    private IEnumerator LowerTheModel()
    {
        float elapsedTime = 0f;

        for (;elapsedTime < _waitTime; elapsedTime += Time.deltaTime)
        {
            _enemy.PivotModel.localPosition = 
            Vector3.Lerp(_enemy.PivotModel.localPosition, _bottomPosition, elapsedTime / _waitTime);
            yield return new WaitForEndOfFrame();
        }
        _enemy.PivotModel.localPosition = _bottomPosition;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator DrawRadiusAttack()
    {
        _imageRadiusAttack.gameObject.SetActive(true);

        float elapsedTime = 0f;
        for (;elapsedTime < _waitTime; elapsedTime += Time.deltaTime)
        {
            _imageRadiusAttack.rectTransform.sizeDelta = 
            Vector3.Lerp(Vector3.zero, _sizeRadiusAttack, elapsedTime / _waitTime);
            yield return new WaitForEndOfFrame();
        }
        _imageRadiusAttack.rectTransform.sizeDelta = _sizeRadiusAttack;
        yield return new WaitForEndOfFrame();
    }

    private void TryAttack()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) <= _radius)
            Target.TakeDamage(_damage);
        
        Instantiate(_effectBlast, transform.position, Quaternion.identity);
        _statistic.AddDiedEnemy();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
