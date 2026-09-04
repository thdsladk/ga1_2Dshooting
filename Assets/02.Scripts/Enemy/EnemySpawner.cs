using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    // Header는 에디터 화면에서 이름을 달아주기 위한 기능.
    // 필요 속성
    // - 타이머
    [Header("스폰 간격")] [SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // - 생성할 프리팹
    [Header("스폰할 적 프리팹")] [SerializeField] private Enemy[] _enemyPrefabs;


    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            _timer = 0;
            _spawnInterval = UnityEngine.Random.Range(1f, 3f); // float : 1f ~ 3f
            int randomInt = Random.Range(1, 3); // int : 1 ~ 2
            Spawn();
        }
    }

    private void Spawn()
    {
        // 단점
        // 1. 세팅한 사람만 뭐가 어떤 프리팹이 들어 있는지 모른다.
        // 2. 각 적 스폰 확률을 매직 넘버로 하드 코딩해서 유지보수가 어렵다.
        // 그래서 !!!!! 
        // Todo: Scriptable Object 를 사용해서 리팩토링
        //
        
        int PerSent = UnityEngine.Random.Range(1, 101);
        int Index = 0;
        if (PerSent < 50)
        {
        }
        else if (PerSent < 80)
        {
            Index = 1;
        }
        else
        {
            Index = 2;
        }

        Enemy enemy = Instantiate(_enemyPrefabs[Index]);
        enemy.transform.position = transform.position;
    }
}