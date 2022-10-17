using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public objectPooler Pooler { get; set; }
    public static DamageTextManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pooler = GetComponent<objectPooler>();
    }
}
