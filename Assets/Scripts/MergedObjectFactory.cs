using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MergedObjectFactory
{
    private readonly MergedObject.Pool _applePool;
    private readonly MergedObject.Pool _pearPool;
    private readonly MergedObject.Pool _bananaPool;

    public MergedObjectFactory([Inject(Id = 0)]MergedObject.Pool applePool,[Inject(Id = 1)] MergedObject.Pool pearPool, [Inject(Id = 2)]MergedObject.Pool bananaPool)
    {
        _applePool = applePool;
        _pearPool = pearPool;
        _bananaPool = bananaPool;
    }

    public MergedObject Spawn(int id)
    {
        MergedObject mergedObject = null;

        switch(id)
        {
            case 0: mergedObject = _applePool.Spawn(_applePool);
                break;
            case 1: mergedObject = _pearPool.Spawn(_pearPool);
                break;
            case 2: mergedObject = _bananaPool.Spawn(_bananaPool);
                break;
        }

        return mergedObject;
    }
}
