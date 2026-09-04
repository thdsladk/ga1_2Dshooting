using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public Vector2 Direction;
    public float _Speed = 10;


    private void Start()
    {
        Direction = Vector2.up;
    }

    private void Update()
    {
        transform.Translate(Direction.normalized * Time.deltaTime * _Speed);
    }
}