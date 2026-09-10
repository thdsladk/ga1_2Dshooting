using System;
using UnityEngine;

// 1차 목표 가장 가까운 적 찾기 

public class PlayerAutoMove : MonoBehaviour
{
    private GameObject[] _enemyArray;

    [SerializeField] private float _moveSpeed = 5f;

    private void Awake()
    {
    }

    private void Update()
    {
        // 1. 타겟을 구한다.
        SearchEnemy();

        // 2. 방향을 구한다.
        Vector3 direction = _enemyArray[0].transform.position - transform.position;
        direction.Normalize();
        direction.y = 0;

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
    }
}