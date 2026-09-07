using System;
using UnityEngine;
using Random = UnityEngine.Random;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _damage;
    [SerializeField] protected GameObject[] _Items;

    private void Update()
    {
        Move();
    }

    protected abstract void Move();


    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return _damage;
    }

    private void Death()
    {
        int persent = Random.Range(0, 10);
        if (persent < 3)
        {
            int ItemIndex = Random.Range(0, 3);
            Instantiate(_Items[ItemIndex], transform.position, transform.rotation);
        }

        Destroy(gameObject);
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