using System;
using UnityEngine;
using Zenject;

public class LaunchController : MonoBehaviour
{
    private MergedObjectFactory _factory;

    [Header("Visual Settings")]
    [SerializeField] private Transform _launchPosition;
    [SerializeField] private int _moveRange;
    [SerializeField] private LayerMask _groundLayer;
    [Header("Upgrade Settings")]
    [SerializeField] private int _maxSpawnIndex;
    [SerializeField] private int _successPercentage;
    [SerializeField] private int _decreasePercentage;
    [Header("Force Settings")]
    [SerializeField] private float _forceMagnitude;

    private Func<int, int, bool> _canSpawnNext;

    private MergedObject _mergedObject;

    private int _prefabIndex;

    [Inject]
    public void Construct(MergedObjectFactory factory) => _factory = factory;
    private void Start() => InitialSettings();
    private void InitialSettings()
    {
        _canSpawnNext = (percentage, successPercentage) => percentage <= successPercentage;
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
                _launchPosition.position = desiredPosition;
                CreatePrefab(desiredPosition);
            }
        }
        else if(Input.GetMouseButton(0))
        {
            //Pressing
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
            {
                _launchPosition.position = new Vector3(hit.point.x, 0f, 0f);

                if (hit.point.x >= _moveRange)
                    _launchPosition.position = new Vector3(_moveRange, 0f, 0f);
                else if (hit.point.x <= -_moveRange)
                    _launchPosition.position = new Vector3(-_moveRange, 0f, 0f);
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
                    _launchPosition.position = new Vector3(_moveRange, 0f, 0f);
                else if (hit.point.x <= -_moveRange)
                    _launchPosition.position = new Vector3(-_moveRange, 0f, 0f);

                _mergedObject.Launch(_forceMagnitude);
                _mergedObject.transform.SetParent(null);
            }
        }
    }
    private void CreatePrefab(Vector3 spawnPosition)
    {
        CalculateChanceToNext();

        _mergedObject = _factory.Spawn(_prefabIndex);
        _mergedObject.transform.position = spawnPosition;
        _mergedObject.transform.SetParent(_launchPosition.transform);
        ResetUpgradeSettings();
    }
    private void CalculateChanceToNext()
    {
        for (int i = 0; i < _maxSpawnIndex; i++)
        {
            var currentPercentage = GetRandomChance();
            Debug.Log($"Current Chance is {currentPercentage}..");
            if (_canSpawnNext(currentPercentage, _successPercentage))
            {
                DecreaseUpgradeChance();
                IncreasePrefabIndex();
                Debug.Log("Upgrade is succesfull!");
            }
            else
            {
                Debug.Log("Upgrading is Failed");
                break;
            }
        }
    }
    private void DecreaseUpgradeChance() => _successPercentage -= _decreasePercentage;
    private void IncreasePrefabIndex() => _prefabIndex++;
    private void ResetUpgradeSettings()
    {
        _successPercentage = 30;
        _prefabIndex = 0;
    }
    private int GetRandomChance()
    {
        return UnityEngine.Random.Range(0, GameConstant.GameSettings.MAX_CHANCE);
    }
}
