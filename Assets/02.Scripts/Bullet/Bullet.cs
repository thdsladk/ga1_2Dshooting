using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Bullet : MonoBehaviour
{
    // 목적: 총알을 위로 움직이고 싶다.
    public float MoveSpeed;
    public int Damage;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource != null)
        {
            _audioSource.pitch = UnityEngine.Random.Range(-0.8f, 2.5f);
            _audioSource.volume = UnityEngine.Random.Range(0.8f, 1.0f);
        }
    }

    private void Start()
    {
        _audioSource.Play();
    }

    private void Update()
    {
        Vector2 direction = Vector2.up; //  new Vector2(0, 1);
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

    // 트리거 관련 이벤트
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 친구가 Enemy일때만 죽여쁠자!
        if (other.gameObject.CompareTag("Enemy"))
        {
            // GetComponent<타입>() -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.LogWarning("enemy is null");
            }

            // 응집도는 높히고, 결합도는 낮춰라
            // 결합도란 묻는거.. 매번 묻는거..
            // 무적모드 검사하고
            // 방어력 검사.. 
            enemy.TakeDamage(Damage);
        }


        // 나죽고!
        Destroy(this.gameObject);
    }
}