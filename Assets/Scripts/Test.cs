using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    private MergedObjectFactory _factory;

    [Inject]
    public void Construct( MergedObjectFactory factory ) => _factory = factory;

    private void Start()
    {
        var appleCount = 2;
        var pearCount = 3;
        var bananaCount = 1;

        for (var i = 0; i < appleCount; i++)
        {
            var applePrefab = _factory.Spawn(0);
        }

        for (var i = 0; i < pearCount; i++)
        {
            var pearPrefab = _factory.Spawn(1);
        }

        for (var i = 0; i < bananaCount; i++)
        {
            var bananaPrefab = _factory.Spawn(2);
        }
    }
}
