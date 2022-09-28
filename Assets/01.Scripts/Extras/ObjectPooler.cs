using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _prefab; // 뭘 preload 할 것인지
    [SerializeField] private int _poolSize = 10; // pool의 크기

    private List<GameObject> _pool; // 배열로 만들어서 관리
    private GameObject _poolContainer; // pool에서 만든 오브젝트 구조화

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
