using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containers : MonoBehaviour
{
    [SerializeField] private GameObject _containerSnakeTail;
    [SerializeField] private GameObject _containerBullet;
    [SerializeField] private GameObject _containerEnemy;
    [SerializeField] private GameObject _containerBonus;

    public GameObject ContainerSnakeTail => _containerSnakeTail;
    public GameObject ContainerBullet => _containerBullet;
    public GameObject ContainerEnemy => _containerEnemy;
    public GameObject ContainerBonus => _containerBonus;
}
