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
    [SerializeField] private EnemySpawnDataTableSO _spawnDataTable;


    private void Start()
    {
    }

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

        // 가중치 랜덤 선택  ( Weight Random Select ) ( 자신의 가중치 / 전체 가중치 )     // 나중에 벨런스 AI 툴 시트 만들것.( 벨런스 자동화 )
        // 각 아이템에 가중치를 부여하고, 가중치가 클수록 높은 확률로 선택되도록 하는 방식 

        // 1. 모두 더한다. 
        int totalWeight = 0;
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        // 2. 전체 가중치 범위에서 랜덤한 정수를 뽑는다.
        int randomWeight = Random.Range(0, totalWeight);

        // 3. 가중치를 누적하면서 선택된 구간을 뽑느다.
        int CumulativeWeight = 0;
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            CumulativeWeight += data.Weight;
            if (randomWeight < CumulativeWeight)
            {
                GameObject enemy = Instantiate(data.EnemyPrefab);
                enemy.transform.position = transform.position;
                break;
            }
        }
    }
}