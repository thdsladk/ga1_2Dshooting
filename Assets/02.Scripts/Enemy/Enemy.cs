using System;
using UnityEngine;
using Random = UnityEngine.Random;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed;

    [SerializeField] protected int _damage;
    private Animator _animator;

    // - 생성할 프리팹
    [Header("생성할 적 프리팹")] [SerializeField] private Item[] _itemPrefabs;

    // - 죽을때 생성할 이펙트 프리펩
    [SerializeField] private GameObject _deathEffectPrefab;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    protected abstract void Move();


    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger("Hit");
        if (_health <= 0)
        {
            Death();
        }
    }

    private void SpawnItem()
    {
        if (Random.Range(0, 100) >= 30) return;
        Instantiate(_itemPrefabs[Random.Range(0, 3)], transform.position, transform.rotation);
        //Debug.Log("아이템!!!!");
    }

    private void Death()
    {
        // 단점
        // 1. 세팅한 사람만 알고 뭐가 어떤 프리팹이 들어 있는지 모른다.
        // 2. 각 적 스폰 확률을 매직 넘버로 하드 코딩해서 유지보수가 어렵다.
        // 그래서 !!!!! 
        // Todo: Scriptable Object 를 사용해서 리팩토링
        //

        SpawnItem();
        SpawnDeathEffect();
        Destroy(gameObject);
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.Log("플레이어를 찾지 못했습니다.");
            }

            // 플레이어
            player.TakeDamage(_damage);
            // 자신
            Death();
        }
    }
}