using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _lives = 10;
    
    public int totalLives { get; set; }

    private void Start()
    {
        totalLives = _lives;

        if (totalLives <= 0)
            totalLives = 0;
    }

    private void ReduceLives()
    {
        totalLives--;
    }

    private void OnEnable() //���ӿ�����Ʈ�� Ȱ��ȭ �ɋ�����
    {
        Enemy.OnEndReached += ReduceLives;
    }

    private void OnDisable() //���ӿ�����Ʈ�� ��Ȱ��ȭ �ɋ�����
    {
        Enemy.OnEndReached -= ReduceLives;
    }
}
