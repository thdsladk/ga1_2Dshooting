using UnityEngine;
using UnityEngine.Serialization;

public class BulletPool : MonoBehaviour
{
    // 오브젝트 풀링이란 : 오브젝트의 Pool(웅동이 : 창고)을 만들어두고, 
    // 그 창고안에 게임 오브젝트를 미리 필요한 만큼 만들어두고,
    // 필요할 때마다 꺼내서 사용하고 필요가 없으면 반환하는 식으로 ( 활성화 / 비활성화 ) 
    // 메모리 할당과 (객체의 생성) 해제(파괴)를 최소화해서 성능 Up! 

    // static(정적)
    private static BulletPool _instance;
    public static BulletPool Instance => _instance;

    // 필요 속성 
    [Header("총알 프리팹")]
    [SerializeField] private Bullet[] _bulletPrefabs;

    [Header("풀 사이즈")]
    [SerializeField] private int _poolSize = 50;

    // 생성한 총알을 담아둘 풀
    private Bullet[] _pool;
    private Bullet[] _poolSub;


    private void Awake()
    {
        // 싱글톤 파트
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 총알 초기화 파트

        // 창고를 창고 크기만큼 생성       // _poolSize는 프리펩 하나당의 크기이다. 
        _pool = new Bullet[_poolSize * _bulletPrefabs.Length];

        // 창고 크기 만큼 총알을 미리 만들어서 집어 넣는다. 
        foreach (Bullet bulletPrefab in _bulletPrefabs)
        {
            for (int i = 0; i < _poolSize; i++)
            {
                Bullet bullet = Instantiate(bulletPrefab, transform);
                bullet.gameObject.SetActive(false); // 비활성화로 시작
                _pool[i] = bullet;
            }
        }
    }

    public Bullet GetBullet(BulletType type)
    {
        foreach (Bullet bullet in _pool)
        {
            if (bullet.BulletType != type)
            {
                continue;
            }
            
            if (bullet.gameObject.activeSelf == false)
            {
                bullet.gameObject.SetActive(true);
                bullet.OnSpawn();
                return bullet;
            }
        }

        return null;
    }
}