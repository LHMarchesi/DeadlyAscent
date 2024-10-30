using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float timeoutDelay = 3f;

    public ObjectPool<Projectile> pool;

    void OnEnable()
    {
        Invoke("Deactivate",timeoutDelay);
    }
    
    private void Deactivate()
    {
        pool.Release(this);
    }
}
