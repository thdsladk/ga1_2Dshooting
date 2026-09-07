using UnityEngine;

public class AimedEnemy : Enemy
{
    private GameObject _player;
    private Vector2 _direction;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.Log("플레이어 태그를 가진 게임 오브젝트를 찾지 못했습니다.");
        }

        _direction = (_player.transform.position - transform.position).normalized;

        //  바라보게 하기 
        transform.up = -_direction;
    }

    protected override void Move()
    {
        if (_player == null)
        {
            Debug.Log("플레이어를 찾지 못했습니다.");
        }

        //  방향과 속도에 맞게 이동한다.
        transform.Translate(_direction * _moveSpeed * Time.deltaTime);
    }
}