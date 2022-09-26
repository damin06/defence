using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{ 
    private Animator _animator;
    private Enemy _enemy;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
    }

    private void PlayDieAnimation()
    {
        _animator.SetTrigger("Die");
    }

    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hurt");
    }

    private float GetcurrentAnimation()
    {
        float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLength;
    }

    private   IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetcurrentAnimation() + 0.5f);
        _enemy.ResumeMovemnet();
    }

    private void EnemyHit(Enemy enemy)
    {
        if(_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }
    private void EnemyDie(Enemy enemy)
    {
        if(_enemy == enemy)
        {
            StartCoroutine(PlayDie());
        }
    }

    private IEnumerator PlayDie()
    {
        _enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetcurrentAnimation() + 0.5f);
        _enemy.ResumeMovemnet();

    }

    private void OnEnable()
    {
        enemyHealth.OnEnemyHit +=EnemyHit;
    }

    private void OnDisable()
    {
        enemyHealth.OnEnemyHit -=EnemyHit;
    }
}
