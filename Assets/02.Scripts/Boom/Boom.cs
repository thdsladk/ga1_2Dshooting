using UnityEngine;

public class Boom : InteractableObject
{
    private float _lifeCycle = 3f;
    private float _BoomCycle = 3f;
    private float _timer = 0f;
    private bool _isExploding = false;

    private float _attackRate = 1000f;

    private Animator _animator;
    private CircleCollider2D _collider;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CircleCollider2D>();

        // 초기 설정
        _collider.isTrigger = false;
    }


    private void Update()
    {
        if (_isExploding == false)
        {
            if (_timer < _lifeCycle)
            {
                _timer += Time.deltaTime;
                transform.localScale = new Vector3(
                    Mathf.Max(0.1f, transform.localScale.x - Time.deltaTime),
                    Mathf.Max(0.1f, transform.localScale.y - Time.deltaTime),
                    transform.localScale.z
                );
            }
            else
            {
                _timer = 0f;
                Explosion();
            }
        }
        else
        {
            if (_timer < _BoomCycle)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                EndExplosion();
            }
        }
    }


    private void Explosion()
    {
        _isExploding = true;

        // 크기 복구
        transform.localScale = Vector3.one * 2f;

        // 충돌 활성화 
        _collider.isTrigger = true;
        // 폭발 애니메이션 재생.
        _animator.SetTrigger("Boomming");
    }

    private void EndExplosion()
    {
        // 충돌 비활성화
        _collider.isTrigger = false;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)_attackRate);
            }
        }
    }
}