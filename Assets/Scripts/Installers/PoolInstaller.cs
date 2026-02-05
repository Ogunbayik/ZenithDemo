using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] private GameObject _pearPrefab;
    [SerializeField] private GameObject _bananaPrefab;
    public override void InstallBindings()
    {
        Container.BindMemoryPool<MergedObject, MergedObject.Pool>()
            .WithId(0)
            .FromComponentInNewPrefab(_applePrefab)
            .UnderTransformGroup("Apples");

        Container.BindMemoryPool<MergedObject, MergedObject.Pool>()
            .WithId(1)
            .FromComponentInNewPrefab(_pearPrefab)
            .UnderTransformGroup("Pears");

        Container.BindMemoryPool<MergedObject, MergedObject.Pool>()
            .WithId(2)
            .FromComponentInNewPrefab(_bananaPrefab)
            .UnderTransformGroup("Bananas");

        Container.Bind<MergedObjectFactory>().AsSingle();
    }
}