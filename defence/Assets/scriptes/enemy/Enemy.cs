using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached;
    [SerializeField] private float moveSpeed = 3.0f;
    private int _currentWaypointIndex;
    //[SerializeField] private Waypoint waypoint;
    public Waypoint waypoint{get; set;}
    public Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(_currentWaypointIndex);

    private enemyHealth _enemyHealth;
    //Vector3 vec;

    private void Update()
    {
        
        //vec = new Vector3(transform.position.x,transform.position.y,transform.position.z);

        // if(CurrentPositionReached())
        // {
        //     if(_currentWaypointIndex >= waypoint.Points.Length)
        //     {
        //         _currentWaypointIndex = 0;
        //     }
        //     else
        //     {
        //         _currentWaypointIndex++;
        //     }
        // }
        if(CurrentPositionReached())
        {
            UpdateCurrentPointIndex();
        }
        Move();
    }

    private void Start()
    {
        _enemyHealth = GetComponent<enemyHealth>();
        _currentWaypointIndex = 0;
    }

    private void Move()
    {
        //Vector3 currentPosition = waypoint.GetWaypointPosition(_currentWaypointIndex);
        transform.position = Vector3.MoveTowards(transform.position,CurrentPointPosition,moveSpeed * Time.deltaTime);
        // if(currentPosition == vec)
        // {
        //     _currentWaypointIndex++;
        // }
    }
    private bool CurrentPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if(distanceToNextPointPosition < 0.1f)
        {
            return true;
        }
        return false;
    }

    private  void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = waypoint.Points.Length - 1;
        if(_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            //enemy가 끝까지 도착했다면 object pooler로 되돌린다
            EndpoolReached();
        }
    }

    private void EndpoolReached()
    {
        if(OnEndReached != null)
        {
            OnEndReached.Invoke();
        }
        
        _enemyHealth.RestHealth();
        objectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }
}
