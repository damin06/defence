using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    public static Action OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;

    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth {get; set;}
    private Image _healthBar;
    void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5);
        }

        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FIllAmountImage;
    }

    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;

        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            //Die
            Die();
        }
    }

    private void Die()
    {
        //CurrentHealth = initialHealth; //체력 수치 리셋
        //_healthBar.fillAmount = 1;  //체력바 이미지 리셋
        RestHealth();
        OnEnemyKilled.Invoke();
        objectPooler.ReturnToPool(gameObject); //오브젝트 플로 되돌아감 씬에서 삭제
    }

    public void RestHealth()
    {
        CurrentHealth = initialHealth;
        _healthBar.fillAmount = 1;
    }
}
