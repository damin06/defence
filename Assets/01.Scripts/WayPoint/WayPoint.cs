using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Vector3[] _points;
    
    public Vector3[] points => _points; // editor에서 사용하기 위해 public 선언
    public Vector3 currentPosition => _currentPosition;

    private Vector3 _currentPosition;
    private bool _gameStarted;

    private void Start()
    {
        _gameStarted = true;
        _currentPosition = transform.position;
    }

    public Vector3 GetWayPointPosition(int index)
    {
        return _currentPosition + _points[index];
    }

    private void OnDrawGizmos()
    {
        if (!_gameStarted && transform.hasChanged)
            _currentPosition = transform.position;

        for (int i = 0; i < _points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_points[i] + _currentPosition, radius: 0.5f);

            if (i < _points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(_points[i] + _currentPosition, _points[i+1] + _currentPosition);
            }
        }
    }
}
