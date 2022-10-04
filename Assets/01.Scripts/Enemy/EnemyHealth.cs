using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action onEnemyKilled;
    public static Action<Enemy> onEnemyHit;

    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Transform _barPosition;

    [SerializeField] private float _initiaHealth = 10f;
    [SerializeField] private float _maxHealth = 10f;

    public float currentHealth { get; set; }

    private Image _healthBar;
    private Enemy _enemy;

    private EnemyAnimation _enemyAnimation;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyAnimation = GetComponent<EnemyAnimation>();
    }

    private void Start()
    {
        CreateHealthBar();
        currentHealth = _initiaHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            DealDamage(5);

        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, currentHealth / _maxHealth, Time.deltaTime * 10f);
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(_healthBarPrefab, _barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.fillAmountImage;
    }

    public void DealDamage(float damageReceived)
    {
        currentHealth -= damageReceived;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            _enemyAnimation.EnemyDead(_enemy);
        }
        else
            onEnemyHit?.Invoke(_enemy);
    }

    public void ResetHealth()
    {
        currentHealth = _initiaHealth;
        _healthBar.fillAmount = 1;
    }

    public void Die()
    {
        ResetHealth();

        onEnemyKilled?.Invoke();

        ObjectPooler.ReturnToPool(gameObject);
    }
}
