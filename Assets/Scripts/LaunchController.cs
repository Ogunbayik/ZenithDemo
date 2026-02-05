using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchController : MonoBehaviour
{
    private const int MAX_CHANCE = 100;

    [Header("Visual Settings")]
    [SerializeField] private GameObject _testLauchPoint;
    [SerializeField] private int _moveRange;
    [SerializeField] private LayerMask _groundLayer;
    [Header("Prefab Settings")]
    [SerializeField] private List<GameObject> _prefabList = new List<GameObject>();
    [SerializeField] private int _prefabIndex;
    [Header("Upgrade Settings")]
    [SerializeField] private int _upgradeCount;
    [SerializeField] private int _upgradeChance;
    [SerializeField] private int _decreaseChance;

    private Func<int, int, bool> CanSpawnNext;

    private GameObject _randomPrefab;
    private void Start() => InitialSettings();
    private void InitialSettings()
    {
        CanSpawnNext = (randomChance, upgradeChance) => randomChance <= upgradeChance;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Start Press
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Object only moving X Axis
                var desiredPosition = new Vector3(hit.point.x, 0f, 0f);
                _testLauchPoint.transform.position = desiredPosition;
                CreateRandomPrefab(desiredPosition);
            }
        }
        else if(Input.GetMouseButton(0))
        {
            //Pressing
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
            {
                _testLauchPoint.transform.position = new Vector3(hit.point.x, 0f, 0f);

                if (hit.point.x >= _moveRange)
                    _testLauchPoint.transform.position = new Vector3(_moveRange, 0f, 0f);
                else if (hit.point.x <= -_moveRange)
                    _testLauchPoint.transform.position = new Vector3(-_moveRange, 0f, 0f);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
            {
                Debug.Log($"Player send object {hit.point}");
                if (hit.point.x >= _moveRange)
                    _testLauchPoint.transform.position = new Vector3(_moveRange, 0f, 0f);
                else if (hit.point.x <= -_moveRange)
                    _testLauchPoint.transform.position = new Vector3(-_moveRange, 0f, 0f);
                _randomPrefab.transform.SetParent(null);
            }
        }
    }
    private void CreateRandomPrefab(Vector3 spawnPosition)
    {
        for (int i = 0; i < _upgradeCount; i++)
        {
            var chance = GetRandomChance();
            Debug.Log($"Current Chance is {chance}");
            if (CanSpawnNext(chance, _upgradeChance))
            {
                DecreaseUpgradeChance();
                IncreasePrefabIndex();
                Debug.Log($"Upgrade is Succesfull");
            }
            else
            {
                Debug.Log("Upgrading is Failed");
                break;
            }
        }

        _randomPrefab = Instantiate(_prefabList[_prefabIndex]);
        _randomPrefab.transform.position = spawnPosition;
        _randomPrefab.transform.SetParent(_testLauchPoint.transform);
        ResetUpgradeSettings();
    }
    private void DecreaseUpgradeChance() => _upgradeChance -= _decreaseChance;
    private void IncreasePrefabIndex() => _prefabIndex++;
    private void ResetUpgradeSettings()
    {
        _upgradeChance = 30;
        _prefabIndex = 0;
    }
    private int GetRandomChance()
    {
        return UnityEngine.Random.Range(0, MAX_CHANCE);
    }
}
