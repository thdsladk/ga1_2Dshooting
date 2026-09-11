using System;
using UnityEngine;

// 1차 목표 가장 가까운 적 찾기 

public class PlayerAutoMove : MonoBehaviour
{
    private GameObject[] _enemyArray;
    [SerializeField] private GameObject _target;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Vector2 _maxPosition;
    [SerializeField] private Vector2 _minPosition;
    private static readonly Vector3 _center = new Vector3(0f, -3f, 0f);


    private void Awake()
    {
    }

    private void Update()
    {
        // 1. 타겟을 구한다.   타겟이 없거나 타겟이 있어도 최소 범위보다 아래일때 
        if (_target == null || _target.transform.position.y < _minPosition.y)
        {
            SearchEnemy();
            CenterReturnMove();
        }

        if (_target != null)
        {
            Move();
        }
    }

    private void CenterReturnMove()
    {
        Vector3 direction = (_center - transform.position).normalized;

        // 3. 속도에 맞게 이동을 한다.
        transform.Translate(direction * Time.deltaTime * _moveSpeed);
    }

    private void Move()
    {
        // 2. 방향을 구한다.
        Vector3 diff = _enemyArray[0].transform.position - transform.position;
        Vector3 direction = diff;


        if (diff.y >= 3)
        {
            direction.y = 1;
        }
        else
        {
            direction.y = -1;
        }

        direction.Normalize();

        // 3. 속도에 맞게 이동을 한다.
        transform.Translate(direction * Time.deltaTime * _moveSpeed);
    }

    private void SearchEnemy()
    {
        _enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        Array.Sort(_enemyArray, (a, b) =>
        {
            float distA = Vector3.Distance(transform.position, a.transform.position);
            float distB = Vector3.Distance(transform.position, b.transform.position);
            return distA.CompareTo(distB); // 가까운 순으로 정렬
        });

        foreach (GameObject target in _enemyArray)
        {
            if (target.transform.position.y >= _minPosition.y)
            {
                _target = target;
                break;
            }
        }
    }
}