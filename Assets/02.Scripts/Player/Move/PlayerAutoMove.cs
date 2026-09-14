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
    [SerializeField] private int _stopTrackingY = 2;


    [SerializeField] private TrailRenderer _LeftTrailRenderer;
    [SerializeField] private TrailRenderer _RightTrailRenderer;


    private void Update()
    {
        // 1. 타겟을 구한다.   타겟이 없거나 타겟이 있어도 최소 범위보다 아래일때 
        if (_target == null || _target.transform.position.y < _minPosition.y)
        {
            FindNearestTarget();
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
        //transform.Translate(direction * Time.deltaTime * _moveSpeed, Space.World);
        float finalSpeed = _moveSpeed + UpgradeManager.Instance.Upgrades[2].CurrentValue;
        transform.position += direction * Time.deltaTime * finalSpeed;
    }

    private void Move()
    {
        // 2. 방향을 구한다.
        Vector3 diff = _target.transform.position - transform.position;
        Vector3 direction = diff;

        // 적과 나와의 y축 차이가 3보다 크면 앞으로 가고 아니면 뒤로가게
        if (diff.y >= 3)
        {
            direction.y = 1;
        }
        else
        {
            direction.y = -1;
        }

        direction.Normalize();

        // Trail Section
        if (direction.y > 0)
        {
            if (_LeftTrailRenderer != null && _RightTrailRenderer != null)
            {
                _LeftTrailRenderer.emitting = true;
                _RightTrailRenderer.emitting = true;
            }
        }
        else
        {
            if (_LeftTrailRenderer != null && _RightTrailRenderer != null)
            {
                _LeftTrailRenderer.emitting = false;
                _RightTrailRenderer.emitting = false;
            }
        }

        // 3. 속도에 맞게 이동을 한다.
        transform.Translate(direction * Time.deltaTime * _moveSpeed);
    }

    private void FindNearestTarget()
    {
        // 1. 타겟을 구한다.
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets.Length == 0) return;

        _target = targets[0];
        float minDistance = float.MaxValue;

        // 1-1. 가장 가까운 타겟을 찾는다.
        foreach (GameObject enemy in targets)
        {
            if (enemy.transform.position.y < -_stopTrackingY)
            {
                continue;
            }

            // 거리를 구해서
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance) // 저장된 거리보다 짧다면
            {
                // 타겟 변경
                minDistance = distance;
                _target = enemy;
            }
        }
    }
}