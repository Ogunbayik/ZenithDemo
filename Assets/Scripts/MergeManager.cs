using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MergeManager : MonoBehaviour
{
    private MergedObjectFactory _factory;
    private SignalBus _signalBus;

    [Header("Merge Settings")]
    [SerializeField] private float _forceMultiplier;

    [Inject]
    public void Construct(MergedObjectFactory factory, SignalBus signalBus) 
    {
        _factory = factory;
        _signalBus = signalBus;
    }
    private void OnEnable() => _signalBus.Subscribe<GameSignal.MergedObjectCollision>(CreateMergedObject);
    private void OnDisable() => _signalBus.Unsubscribe<GameSignal.MergedObjectCollision>(CreateMergedObject);
    public void CreateMergedObject(GameSignal.MergedObjectCollision signal)
    {
        var nextObjetID = signal.MergedObjectID + 1;

        var nextObject = _factory.Spawn(nextObjetID);
        nextObject.transform.position = new Vector3(signal.HitPosition.x, 0f, signal.HitPosition.z);
        nextObject.Launch(_forceMultiplier);
    }
}
