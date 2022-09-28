using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _prefab; // �� preload �� ������
    [SerializeField] private int _poolSize = 10; // pool�� ũ��

    private List<GameObject> _pool; // �迭�� ���� ����
    private GameObject _poolContainer; // pool���� ���� ������Ʈ ����ȭ

    private void Awake()
    {
        _pool = new List<GameObject>();

        _poolContainer = new GameObject(name: $"Pool-{_prefab.name}");

        CreatePooler();
    }

    private void CreatePooler()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            _pool.Add(item: CreateInstance());
        }
    }

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(_prefab);
        newInstance.transform.SetParent(_poolContainer.transform);

        newInstance.SetActive(false);
        return newInstance;
    }

    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                print(1);
                return _pool[i];
            }
        }
        return CreateInstance();
    }

    public static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
    }
}