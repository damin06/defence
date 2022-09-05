

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]private Vector3 [] points;
    public Vector3[] Points => points;

    private Vector3 _currentPosition;
    public Vector3 CurrentPosition => _currentPosition;

    private bool _gamestarted;
    
    void Start()
    {
        _gamestarted = true;
        _currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDrawGizmos()
    {
        if(!_gamestarted && transform.hasChanged)
        {
            _currentPosition = transform.position;
        }
        for(int i =0; i<points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i] + _currentPosition, radius: 0.5f);

            if(i<points.Length -1)
            {
                Gizmos.color=Color.gray;
                Gizmos.DrawLine(points[i] + _currentPosition,points[i+1] + _currentPosition);
            }
        }
    }
}
