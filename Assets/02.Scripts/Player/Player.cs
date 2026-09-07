using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed = 1f;

    // 멤버 컴포넌트 자리
    [SerializeField] private InteractiveComponent _interactableComponent;
    //[SerializeField] private PlayerMove _playerMove;

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

    public void Heal(float healAmount)
    {
        if (healAmount < 0)
        {
            Debug.LogWarning("힐량은 음수 불가");
        }

        _health += healAmount;
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    public float GetDamage(float damage)
    {
        return damage;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        _attackSpeed = attackSpeed;
    }


    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogWarning("데미지는 음수 불가");
        }
    }
}