using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
    {
        Fixed,
        Random
    }
public class spawner : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField]private int enemyCount = 10;
    [SerializeField]private SpawnModes spawnmodes = SpawnModes.Fixed;
    [SerializeField]private GameObject testGO;

    [Header("Fixed Delay")]
    [SerializeField]private float delayBtwSpawns;


    [SerializeField]private float minRandomDelay;
    [SerializeField]private float maxRandomDelay;

//pooler 선언
    private objectPooler _pooler;


    private float _spawnTimer;
    private float _enemiesSpawned;

    void Start()
    {
        _pooler = GetComponent<objectPooler>();
    }
    void Update()
    {
        if(spawnmodes == SpawnModes.Fixed)
        {
            _spawnTimer -= Time.deltaTime;
        if(_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            if(_enemiesSpawned <enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
        if(_spawnTimer < 0)
        {
            _spawnTimer = GetRandomDelay();
            if(_enemiesSpawned <enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
        }
    }
    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstaceFromPool();
        //Instantiate(testGO, transform.position, Quaternion.identity);

        newInstance.gameObject.SetActive(true);
    }
    private float GetRandomDelay()
    {
        float randomTimer = UnityEngine.Random.Range(minRandomDelay,maxRandomDelay);
        return randomTimer;
    }
}
