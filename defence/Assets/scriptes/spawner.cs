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
    [SerializeField]private float delayBtwWaves = 1f;

    [Header("Fixed Delay")]
    [SerializeField]private float delayBtwSpawns;


    [SerializeField]private float minRandomDelay;
    [SerializeField]private float maxRandomDelay;

//pooler 선언
    private objectPooler _pooler;
    private Waypoint _waypoint;


    private float _spawnTimer;
    private float _enemiesSpawned;
    private int _enemiesRemainnig;

    void Start()
    {
        _pooler = GetComponent<objectPooler>();
        _waypoint = GetComponent<Waypoint>();

        _enemiesRemainnig = enemyCount; //10개
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

        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.waypoint = _waypoint;
        enemy.transform.localPosition = transform.position;
        
        enemy.ResetEnemy();
        newInstance.gameObject.SetActive(true);
    }
    private float GetRandomDelay()
    {
        float randomTimer = UnityEngine.Random.Range(minRandomDelay,maxRandomDelay);
        return randomTimer;
    }
    
    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwSpawns);
        _enemiesRemainnig = enemyCount;
        _spawnTimer = 0;
        _enemiesSpawned = 0;
    }

    private void RecordEnmy()
    {
        _enemiesRemainnig--;
        if(_enemiesRemainnig <= 0)
        {
            //새 wave 시작
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached +=RecordEnmy;
        enemyHealth.OnEnemyKilled += RecordEnmy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -=RecordEnmy;
        enemyHealth.OnEnemyKilled -= RecordEnmy;
    }
}
