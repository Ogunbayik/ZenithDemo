using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [Header("Merged Object Settings")]
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] private GameObject _pearPrefab;
    [SerializeField] private GameObject _bananaPrefab;
    public override void InstallBindings()
    {
        Container.BindMemoryPool<MergedObject, MergedObject.Pool>()
            .WithId(GameConstant.MergedObjectIndex.APPLE_INDEX)
            .FromComponentInNewPrefab(_applePrefab)
            .UnderTransformGroup(GameConstant.MergedObjectTransformGroup.APPLE_GROUP);

        Container.BindMemoryPool<MergedObject, MergedObject.Pool>()
            .WithId(GameConstant.MergedObjectIndex.PEAR_INDEX)
            .FromComponentInNewPrefab(_pearPrefab)
            .UnderTransformGroup(GameConstant.MergedObjectTransformGroup.PEAR_GROUP);

        Container.BindMemoryPool<MergedObject, MergedObject.Pool>()
            .WithId(GameConstant.MergedObjectIndex.BANANA_INDEX)
            .FromComponentInNewPrefab(_bananaPrefab)
            .UnderTransformGroup(GameConstant.MergedObjectTransformGroup.BANANA_GROUP);

        Container.Bind<MergedObjectFactory>().AsSingle();
    }
}