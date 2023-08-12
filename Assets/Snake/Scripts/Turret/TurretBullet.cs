using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class TurretBullet : MonoBehaviour
{
    [SerializeField] private GameObject _modelBullet;
    [Space(5)]
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 50;
    [SerializeField] private float _timeLife;
    
    private AudioSource _hitMarkerEffect;
    private Transform _target;
    private Rigidbody _rigidbody;
    private float _countTime;

    private void Awake()
    {
        _countTime = _timeLife;
        _hitMarkerEffect = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        if (_countTime <= 0)
        {
            _rigidbody.isKinematic = true;
            SetDeactivate();
        }

        _countTime -= Time.deltaTime;
    }

    public void ShotBullet(Transform target)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _target = target;
        _countTime = _timeLife;

        Vector3 direction = _target.position - transform.position;
        transform.forward = direction;

        ShowBullet(true);
        _rigidbody.AddForce(direction * _speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            _hitMarkerEffect?.Play();
            enemy.TakeDamage(_damage);
        }
        ShowBullet(false);
        Invoke(nameof(SetDeactivate), 0.2f);
    }

    private void ShowBullet(bool isHide)
    {
        _rigidbody.isKinematic = !isHide;
        _modelBullet.SetActive(isHide);
    }

    private void SetDeactivate()
    {
        gameObject.SetActive(false);
    }
}
