using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header ("SpawnMode")]
    [SerializeField] private SpawnModes _spawnMode = SpawnModes.Fixed;
    [SerializeField] private int _enemyCnt = 10;

    [SerializeField] private float _delayBtnWaves = 1f;
    
    [Header ("SpawnDelay")]
    [SerializeField] private float _minRandomDelay;
    [SerializeField] private float _maxRandomDelay;
    [SerializeField] private float _spawnTimeInt = 2;

    private ObjectPooler _pooler;
    private WayPoint _waypoint;

    private float _spawnTime;
    private float _enemiesSpawned;
    private int _enemiesRemaining;

    private void Awake()
    {
        _pooler = GetComponent<ObjectPooler>();
        _waypoint = GetComponent<WayPoint>();
    }

    private void Start()
    {
        _enemiesRemaining = _enemyCnt;
    }

    private void Update()
    {
        _spawnTime += Time.deltaTime;

        if (_spawnTime > GetSpawnDelay() && _enemiesSpawned < _enemyCnt)
        {
            _spawnTime = 0;
            SpawnEnemy();
            _enemiesSpawned++;
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();

        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.waypoint = _waypoint;

        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;

        newInstance.SetActive(true);
    }

    private float GetSpawnDelay()
    {
        if (_spawnMode == SpawnModes.Random)
        {
            float randomTimer = Random.Range(_minRandomDelay, _maxRandomDelay);
            return randomTimer;
        }
        
        else if (_spawnMode == SpawnModes.Fixed)
            return _spawnTimeInt;

        else
        {
            Debug.LogError("Spawner의 SpawnMode이 설정 되지 않은 값입니다");
            return 999;
        }
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(_delayBtnWaves);
        _enemiesRemaining = _enemyCnt;
        _spawnTime = 0f;
        _enemiesSpawned = 0;
    }

    private void RecordEnemy()
    {
        _enemiesRemaining--;

        if (_enemiesRemaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.onEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.onEnemyKilled -= RecordEnemy;
    }
}
