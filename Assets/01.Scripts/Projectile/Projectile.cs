using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    private Enemy _enemyTarget;

    private void Update()
    {
        if (_enemyTarget != null)
            MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, _moveSpeed * Time.deltaTime);
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemyTarget = enemy;
    }
}