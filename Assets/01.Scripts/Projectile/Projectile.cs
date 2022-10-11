using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private Enemy _emenyTarget;

    [SerializeField] private float damage = 2f;
    [SerializeField] private float minDisToDealDamage = 0.1f;

    public TurretProjectile TurretOwner { get; set; }

    private void Update()
    {
        if(_emenyTarget != null)
        {
            MoveProjectile();
            RotateProjectile();
        }
    }

    private void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            _emenyTarget.transform.position, moveSpeed * Time.deltaTime);

        float disToTarget = (_emenyTarget.transform.position - transform.position).magnitude;
        if(disToTarget < minDisToDealDamage) //적과 총알 거리 충분히 가까움. 총알 맞음
        {
            _emenyTarget.EnemyHealth.DealDamage(damage);

            TurretOwner.ResetTurretProjectile();
            objectPooler.ReturnToPool(gameObject); //총알 맞으면 오브젝트 풀러로 되돌림
        }
    }

    public void SetEnemy(Enemy enemy)
    {
        _emenyTarget = enemy;
    }

    private void RotateProjectile()
    {
        Vector3 enemyPos = _emenyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }
}
