using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private Transform _projectileSpawnPos;
    private ObjectPooler _pooler;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.G))
            LoadProjectile();
    }

    private void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.parent = _projectileSpawnPos;
        newInstance.transform.SetParent(_projectileSpawnPos);
        newInstance.SetActive(true);
    }
}
