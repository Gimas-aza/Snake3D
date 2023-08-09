using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
    [SerializeField] private float timeDestroy;

    void Start()
    {
        Destroy(gameObject, timeDestroy);   
    }
}
