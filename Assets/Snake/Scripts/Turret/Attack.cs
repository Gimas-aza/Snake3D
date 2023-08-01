using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform[] _firePoints;
    [SerializeField] private float _fireRate;
    private int _currentBullet = 0;
    private float _countdown;

    public void Shot(Transform target, List<TurretBullet> bulletList)
    {
        if(_countdown <= 0)
        {
            if (_currentBullet == bulletList.Count)
            {
                _currentBullet = 0;
            }

            for(int i = 0; i < _firePoints.Length; i++)
            {
                TurretBullet bullet = bulletList[_currentBullet++];
                bullet.gameObject.SetActive(true);
                bullet.transform.position = _firePoints[i].position;
                bullet.transform.rotation = _firePoints[i].rotation;

                bullet.ShotBullet(target);
            }

            _countdown = 1 / _fireRate;
        }

        _countdown -= Time.deltaTime;
    }
}
