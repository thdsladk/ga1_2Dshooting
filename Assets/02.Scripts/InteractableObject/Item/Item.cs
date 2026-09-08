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
    protected const float _maxMoveSpeed = 6f;
    [SerializeField] protected const float _itemMoveSpeed = 1f;
    [SerializeField] private float _buffScale = 0f;
    [SerializeField] private ItemType _type;
    [SerializeField] private float _startDelayTime = 3f;

    // Idle Motion 관련 변수
    [SerializeField] private float _idleAmplitude = 0.2f; // 위아래 움직임 크기
    [SerializeField] private float _idleFrequency = 2f; // 움직임 속도
    [SerializeField] private float _applyRadius = 1f;
    private Vector2 _startPosition;
    private Player _player = null;

    private bool _isChase = false;

    // 베지어 곡선 관련 함수
    private float _elapsed = 0f;
    private float _moveDuration = 3f;

    // Effect 
    [SerializeField] private GameObject _receiveEffectPrefab;
    [SerializeField] private GameObject _ShinyEffectPrefab;

    // EffectInstance
    private GameObject _receiveEffectInstance;


    private void Start()
    {
        _startPosition = transform.position;

        // 시작 하자마자 빛나도록 처리.
        _receiveEffectInstance = Instantiate(_ShinyEffectPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        Move();

        if (_receiveEffectInstance != null)
        {
            _receiveEffectInstance.transform.position = transform.position;
        }
    }

    //public float GetItem

    /// <summary>
    /// 아이템을 위아래로 생동감 있게 움직이는 메서드
    /// </summary>
    private void IdleMotion()
    {
        //float newY = _startPosition.y + Mathf.Sin(Time.deltaTime * _idleFrequency) * _idleAmplitude;
        //transform.position = new Vector2(_startPosition.x, newY);
        transform.Translate(-1 * transform.up * _itemMoveSpeed * Time.deltaTime);
    }

    protected void Move()
    {
        if (_startDelayTime > 0f)
        {
            // 시작 딜레이 N초 동안은 아래로 이동
            _startDelayTime -= Time.deltaTime;
            IdleMotion();
        }
        else
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (_player != null)
            {
                Vector2 center = (_player.transform.position + transform.position) / 2f;
                float curveSign = 1f;
                if (_elapsed < 1.5f)
                {
                    CalculateBezierCurve(transform.position, center, curveSign);
                }
                else
                {
                    CalculateBezierCurve(center, _player.transform.position, -curveSign);
                }

                if (_elapsed > _moveDuration)
                {
                    _elapsed = 0f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 여기서는 아이템 값을 받고 아이템 오브젝트를 소멸 
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogWarning("플레이어 태그 오브젝트에 플레이어 컴포넌트가 없습니다.");
            }

            // Effect Section 
            Instantiate(_receiveEffectPrefab, transform.position, Quaternion.identity);
            // Sniny Effect Instance Free
            Destroy(_receiveEffectInstance);

            // 대력 타입이 7개 이상이면 상속으로 구현해라. 
            switch (_type)
            {
                // 심화 과제 1. 퍼사드 패턴 ( 패턴 이란 : 객체지향에서 자주 일어나는 설계 문제를 잘 풀어내도록 경험에 의해 정리한 공식 )
                // 심화 과제 2. 컴포지드 패턴 ( 아이템 종류가 조합에 의해 폭발적으로 증가할 경우  조합 패턴 사용 ) 
                // 포트폴리오에서 가장 중요한게 게임 구현 완성도 ( 코드의 완성도는 가장 후순위 ) 
                // - 게임 개발은 내가 생각한 바를 먼저 구현할 수 있는가 
                // !!! 구현 가능 불가능이 코드 완성도 "보다"는 중요하다 !!! [ 완성도 만을 위해서 구현을 못하는 문제는 회피하자 ] 
                case ItemType.Heal:
                {
                    player.Heal(_buffScale);
                    break;
                }
                case ItemType.MoveSpeed:
                {
                    // 캡슐화 : 
                    // + 데이터 은닉(Speed 속성 private 처리)
                    // + 행위를 통한 상태 변경 (SpeedUp 호출)
                    other.gameObject.GetComponent<PlayerMove>().SpeedUp(_buffScale);
                    break;
                }
                case ItemType.FireRateUp:
                {
                    player.SetDamage(_buffScale);
                    break;
                }
            }

            Destroy(gameObject);
        }
    }

    private void CalculateBezierCurve(Vector2 startPoint, Vector2 endPoint, float curveDirectionX = 1f)
    {
        // 시작점, 제어점, 끝점 정의
        Vector2 direction = (endPoint - startPoint);
        Vector2 normal = new Vector2((-direction.y * curveDirectionX), direction.x).normalized;
        float curveScale = 3f;
        Vector2 controlPoint = (direction / 2f) + (normal * curveScale); // 중간 제어점

        // t 값 (0 ~ 1)
        _elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(_elapsed / (_moveDuration / 2f));

        // 베지어 곡선 공식 (Quadratic Bezier)
        Vector2 bezierPosistion = Mathf.Pow(1 - t, 2) * startPoint
                                  + 2 * (1 - t) * t * controlPoint
                                  + Mathf.Pow(t, 2) * endPoint;

        // 오브젝트 위치 갱신
        transform.position = bezierPosistion;
    }
}