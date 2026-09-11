using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    [SerializeField] private Boom _boomPrefab;
    [SerializeField] private GameObject _boomFailPrefab;


    // - 생성 위치(총구)
    public Transform LeftFirePoint;
    public Transform RightFirePoint;
    public Transform SubLeftFirePoint;
    public Transform SubRightFirePoint;

    // Count 
    [SerializeField] private int _boomAmount = 3;

    // - 쿨타이머
    public float CoolTime = 0.5f;
    public float CoolTimer = 0;

    [SerializeField] float _boomCooldown = 3;
    [SerializeField] float _boomTimer = 0f;

    // - 오토 모드
    public bool AutoFireMode = false;

    private void Start()
    {
        CoolTimer = CoolTime;
    }


    private void Update()
    {
        // 오토 공격 모드 토글
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoFireMode = !AutoFireMode;
        }

        // 0. 쿨타이머 감소
        CoolTimer -= Time.deltaTime;

        // 1. 쿨타이머가 0초 이하이고 && (스페이스바를 누르거나 || 오토 모드라면)
        if (CoolTimer <= 0 && (Input.GetKeyDown(KeyCode.Space) || AutoFireMode))
        {
            // 2. 발사
            Fire();

            // 3. 쿨타이머 초기화
            CoolTimer = CoolTime;
        }

        if (_boomTimer > 0)
        {
            _boomTimer -= Time.deltaTime;
        }

        // Boom Check
        if (Input.GetKeyDown(KeyCode.B))
        {
            Boom();
        }
    }

    private void Fire()
    {
        // 2. 총알 프리팹을 생성한다.
        // ObjectPool로 생성 
        Bullet leftBullet = BulletPool.Instance.GetBullet(BulletType.Main);
        leftBullet.transform.position = LeftFirePoint.position; // 생성한 총알의 위치를 총구의 위치로

        Bullet rightBullet = BulletPool.Instance.GetBullet(BulletType.Main);
        rightBullet.transform.position = RightFirePoint.position; // 생성한 총알의 위치를 총구의 위치로

        Bullet SubleftBullet = BulletPool.Instance.GetBullet(BulletType.Sub);
        SubleftBullet.transform.position = SubLeftFirePoint.position; // 생성한 총알의 위치를 총구의 위치로

        Bullet SubrightBullet = BulletPool.Instance.GetBullet(BulletType.Sub);
        SubrightBullet.transform.position = SubRightFirePoint.position; // 생성한 총알의 위치를 총구의 위치로
    }

    private void Boom()
    {
        if (_boomAmount > 0 && _boomTimer <= 0)
        {
            // Cooldown Setting
            _boomTimer = _boomCooldown;
            // boom Create
            Instantiate(_boomPrefab, gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("폭탄이 없거나 쿨다운이 돌고 있습니다.");
        }
    }
}