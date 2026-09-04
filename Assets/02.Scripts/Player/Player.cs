using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private int _damage;

    // 멤버 컴포넌트 자리
    [SerializeField]
    In

    //private void Start()
    //{
    //}

    //private void Update()
    //{
    //}

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyBuff(BuffType buffType, float buffScale)
    {
        switch (buffType)
        {
            case BuffType.Heal:
            {
                _health
                break;
            }
            case BuffType.AttackSpeedUp:
            {
                break;
            }
            case BuffType.MoveSpeedUp:
            {
                break;
            }
        }
    }
}