using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MergedObject : MonoBehaviour, IPoolable<IMemoryPool>
{
    private Rigidbody _rb;
    private IMemoryPool _pool;

    [Header("Identity Settings")]
    [SerializeField] private int _id;
    private void Awake() => _rb = GetComponent<Rigidbody>();

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
    }
    public void OnDespawned()
    {
        _pool = null;
    }
    public void ReturnToPool()
    {
        _pool.Despawn(this);
    }
    
    private void Launch(float force) => _rb.AddForce(Vector3.forward *  force, ForceMode.Impulse);

        
    public class Pool : MonoPoolableMemoryPool<IMemoryPool, MergedObject> { }
    
}
