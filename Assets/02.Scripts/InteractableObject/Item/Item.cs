using UnityEngine;
using UnityEngine.Serialization;

/*
    아이템을 구현해주세요. (3가지 타입 - 모양과 컬러가 다르다.)

    적을 죽이면 30% 확률로 3가지 타입 아이템 중 하나가 생성됩니다. (프리팹 -> 확률 -> Istantiate)
    아이템은 N초간 멈춰 있다가. N초가 지나면 플레이어를 향해 이동합니다. (타이머, 방향, 이동)
    아이템과 플레이어가 충돌을 하면 효과가 발생합니다. (충돌 처리, 협력)
    플레이어 공격 속도 증가
    플레이어 체력 증가
    플레이어 이동 속도 증가

 */

public class Item : InteractableObject
{
    [SerializeField] protected float _itemMoveSpeed = 1f;
    [SerializeField] protected float _buffScale = 0f;

    // Idle Motion 관련 변수
    [SerializeField] private float _idleAmplitude = 0.2f; // 위아래 움직임 크기
    [SerializeField] private float _idleFrequency = 2f; // 움직임 속도
    [SerializeField] private float _applyRadius = 1f;
    private Vector2 _startPosition;
    private float _startDelayTime = 3f;
    private Player _player = null;

    private bool _isChase = false;


    private void Start()
    {
    }

    private void Update()
    {
        if (_startDelayTime > 0f)
        {
            _startDelayTime -= Time.deltaTime;
        }
        else
        {
            if (_isChase == false)
            {
                IdleMotion();
            }
            else
            {
                Move();
            }
        }
    }

    public float GetItem

    /// <summary>
    /// 아이템을 위아래로 생동감 있게 움직이는 메서드
    /// </summary>
    private void IdleMotion()
    {
        float newY = _startPosition.y + Mathf.Sin(Time.deltaTime * _idleFrequency) * _idleAmplitude;
        transform.position = new Vector2(_startPosition.x, newY);
    }

    protected void Move()
    {
        if (_isChase == false)
        {
            transform.Translate(-1 * transform.up * _itemMoveSpeed * Time.deltaTime);
        }
        else
        {
            if (_player != null)
            {
                Vector2 direction = (_player.transform.position - transform.position).normalized;

                float speedPerSecond = Time.deltaTime * _itemMoveSpeed;
                float acceleration = Mathf.Lerp(_startPosition.x, _player.transform.position.x, speedPerSecond);
                transform.Translate(direction * acceleration);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject.GetComponent<Player>();
            _isChase = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_player != null)
            {
                Vector2 playerPosition = _player.transform.position;
                Vector2 itemPosition = transform.position;
                //Vector2 direction = (playerPosition - itemPosition).normalized;
                float distance = Vector2.Distance(playerPosition, itemPosition);

                if (distance <= _applyRadius)
                {
                    float timer = 0f;
                    timer += (_itemMoveSpeed * Time.deltaTime);
                    transform.position = Vector2.Lerp(itemPosition, playerPosition, timer);
                    //transform.Translate(Direction * Time.deltaTime * _itemMoveSpeed);
                }
                else
                {
                }
            }
        }
    }
}