using UnityEngine;
using UnityEngine.Serialization;

public class ItemPool : MonoBehaviour
{
    // 오브젝트 풀링이란 : 오브젝트의 Pool(웅동이 : 창고)을 만들어두고, 
    // 그 창고안에 게임 오브젝트를 미리 필요한 만큼 만들어두고,
    // 필요할 때마다 꺼내서 사용하고 필요가 없으면 반환하는 식으로 ( 활성화 / 비활성화 ) 
    // 메모리 할당과 (객체의 생성) 해제(파괴)를 최소화해서 성능 Up! 

    // static(정적)
    private static ItemPool _instance;
    public static ItemPool Instance => _instance;

    // 필요 속성 
    [Header("파이템 프리팹")]
    [SerializeField] private Item[] _itemPrefabs;

    [Header("풀 사이즈")]
    [SerializeField] private int _poolSize = 50;

    // 생성한 아이템을 담아둘 풀
    private Item[,] _pool;

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

        // 아이템 초기화 파트

        // 창고를 창고 크기만큼 생성       // _poolSize는 프리펩 하나당의 크기이다. 
        _pool = new Item[_itemPrefabs.Length, _poolSize];

        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            Item itemPrefab = _itemPrefabs[i]; // [ ]
            for (int j = 0; j < _poolSize; j++)
            {
                Item item = Instantiate(itemPrefab, transform);
                item.gameObject.SetActive(false); // 비활성화로 시작
                _pool[i, j] = item;
            }
        }
    }

    public Item GetItem(ItemType type)
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            if (_pool[i, 0].ItemType != type)
            {
                continue;
            }

            for (int j = 0; j < _poolSize; j++)
            {
                Item item = _pool[i, j];

                if (item.gameObject.activeSelf == false)
                {
                    item.gameObject.SetActive(true);
                    item.OnSpawn();
                    return item;
                }
            }
        }


        return null;
    }
}