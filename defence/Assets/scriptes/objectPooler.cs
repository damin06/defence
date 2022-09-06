using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{
    [SerializeField]private GameObject prefab;//preload 할 것인지
    [SerializeField]private int poolSize = 10;//pooler 크기

    private List<GameObject> _pool; //배열로 만들어서 오브젝트 관라
    private GameObject _poolContainer;

    private void Awake()
    {
        _pool = new List<GameObject>();

        _poolContainer = new GameObject(name: $"Pool-{prefab.name}");
        CreatePooler();
    }

    private void CreatePooler()
    {
        for(int i=0; i<poolSize; i++)
        {
            _pool.Add(item: CreateInstance());
        }
    }
    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab);
        newInstance.transform.SetParent(_poolContainer.transform);
        
        newInstance.SetActive(false);
        return newInstance;
    }

    public GameObject GetInstaceFromPool()
    {
        for(int i=0; i<_pool.Count; i++)
        {
            if(!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }
        return CreateInstance();
    }
}
