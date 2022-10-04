using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _attackRange = 3.0f;

    private bool _gameStarted;

    private List<Enemy> _enemies; 

    public Enemy currentEnemyTarget { get; set; }

    private void Start()
    {
        _gameStarted = true;
        _enemies = new List<Enemy>();
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    private void OnDrawGizmos()
    {
        if (!_gameStarted)
            GetComponent<CircleCollider2D>().radius = _attackRange;

        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy newenemy = collision.GetComponent<Enemy>();
            _enemies.Add(newenemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy newEnemy = collision.GetComponent<Enemy>();
            if (_enemies.Contains(newEnemy))
                _enemies.Remove(newEnemy);
        }
    }

    private void GetCurrentEnemyTarget()
    {
        if (_enemies.Count <= 0)
        {
            currentEnemyTarget = null;
            return;
        }

        currentEnemyTarget = _enemies[0];
    }

    private void RotateTowardsTarget()
    {
        if (currentEnemyTarget == null)
            return;

        Vector3 targetPos = currentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }
}
