using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed = 1f;

    // 멤버 컴포넌트 자리
    [SerializeField] private InteractiveComponent _interactableComponent;
    [SerializeField] private PlayerMove _playerMove;

    //private void Start()
    //{
    //}

    //private void Update()
    //{
    //}

    public InteractiveComponent GetInteractiveComponent()
    {
        return _interactableComponent;
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    public void SetDamage(int damage)
    {
        _health = damage;
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        _attackSpeed = attackSpeed;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        // 접근이 위험 하다.
        _playerMove.Speed = moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 여기서는 아이템 값을 받고 아이템 오브젝트를 소멸 
        if (other.CompareTag("Item"))
        {
            other.gameObject.GetComponent<Item>()
        }
    }
}