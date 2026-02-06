using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MergedObject : MonoBehaviour, IPoolable<IMemoryPool>
{
    private SignalBus _signalBus;


    private Rigidbody _rb;
    private IMemoryPool _pool;

    [Header("Identity Settings")]
    [SerializeField] private int _id;

    [Inject]
    public void Construct(SignalBus signalBus) => _signalBus = signalBus;
    private void Awake() => _rb = GetComponent<Rigidbody>();
    public void OnSpawned(IMemoryPool pool) => _pool = pool;
    public void OnDespawned() => _pool = null;
    public void ReturnToPool() => _pool.Despawn(this);
    public void Launch(float force) => _rb.AddForce(Vector3.forward *  force, ForceMode.Impulse);

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<MergedObject>(out MergedObject mergedObject))
        {
            //Ýki objeyide pool'a gönderip yeni bir obje üreteceðiz.
            var contactPoint = collision.contacts[0].point;
            

            if (_id == mergedObject._id)
            {
                if (this.gameObject.GetInstanceID() < mergedObject.gameObject.GetInstanceID())
                {
                    Debug.Log($"{this.gameObject.name} ID is less than {mergedObject.gameObject.name}");
                    _signalBus.Fire(new GameSignal.MergedObjectCollision(_id, contactPoint));

                    mergedObject.ReturnToPool();
                    ReturnToPool();
                }
            }
        }
    }
    public class Pool : MonoPoolableMemoryPool<IMemoryPool, MergedObject> { }
    
}
