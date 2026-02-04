using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int _upgradeChance = 30;
    private int _prefabIndex = 0;
    private int _upgradeCount = 3;

    [SerializeField] private List<GameObject> _prefabList = new List<GameObject>();

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CreateRandomItem();
    }

    private void CreateRandomItem()
    {
        //3 kez yükseltme imkanýmýz olsun
        

       
    }
  
    
}