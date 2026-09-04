using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Header는 에디터 화면에서 이름을 달아주기 위한 기능.
    // 필요 속성
    // - 타이머
    [Header("스폰 간격")] [SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // - 생성할 프리팹
    [Header("스폰할 적 프리팹")] [SerializeField] private Enemy _enemyPrefab;


    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            _timer = 0;
            _spawnInterval = UnityEngine.Random.Range(1f, 3f);  // float : 1f ~ 3f
            int randomInt = Random.Range(1, 3);                 // int : 1 ~ 2
            Spawn();
        }
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = transform.position;
    }
}