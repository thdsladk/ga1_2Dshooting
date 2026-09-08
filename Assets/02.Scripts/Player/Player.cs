using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _attackSpeed = 1f;

    // 멤버 컴포넌트 자리
    [SerializeField] private InteractiveComponent _interactableComponent;

    // Effect Section
    // - 죽을때 생성할 이펙트 프리펩
    [SerializeField] private GameObject _deathEffectPrefab;


    [SerializeField] private Transform  _LeftFirePoint;
    [SerializeField] private Transform  _RightFirePoint;

    // C#의 프로퍼티 문법
    //public float Health
    //{
    //    get => _health;
    //    //set => _health = value;
    //    //get {retrun _health;}
    //    
    //}
    public float Health => _health; // 람다식 문법 ( 프로퍼티 ) 

    public InteractiveComponent GetInteractiveComponent()
    {
        return _interactableComponent;
    }

    public void Heal(float healAmount) // 도메인 지향 메세지 
    {
        if (healAmount < 0)
        {
            Debug.LogWarning("힐량은 음수 불가");
        }

        _health += healAmount;
    }

    // 잘 설계된 클래스는 
    // - 필드  ( 인스턴스 변수)
    // - 필드에 잘못된 값이 할당되지 않게 막고, 정상적으로 동작하는 메서드 
    //public void SetHealth(float health)     // 기술 지향 메서드 
    //{
    //    // 무결성 검사를 해야 한다.
    //    // 무결성 : 잘못된 데이터가 들어가지 않게 하는 것
    //    // - 최대 체력보다 체력은 적어야 한다.
    //
    //    _health = health;
    //}
    //public float GetHealth()
    //{
    //    return _health;
    //}

    public float GetDamage(float damage)
    {
        return damage;
    }

    public void SetDamage(float damage)
    {
        _attackRate = damage;
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
                Death();
            }
        }
        else
        {
            Debug.LogWarning("데미지는 음수 불가");
        }
    }

    private void Death()
    {
        // Effet 
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);

        // Destroy
        Destroy(gameObject);
    }
}