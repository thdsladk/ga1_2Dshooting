using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 필요 속성
    // - xkdlaj
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
            Spawn();
        }
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = transform.position;
    }
}