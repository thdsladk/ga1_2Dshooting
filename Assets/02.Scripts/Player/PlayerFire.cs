using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public Transform FirePointLeft;
    public Transform FirePointRight;

    
     private void Start()
    {
        
    }

     private void Update()
    {
        FireBullet();
    }
     
    private void FireBullet()
    {
        // 1. 스페이스 바를 누르면 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 2. 총알 프리펩 생성 
            // Instantiate는 프리팹을 복사해서 (Monobehavior를 상속받는) 게임 오브젝트를 생성하는 씬에 넣어주는 기능
            GameObject bulletLeft = Instantiate(bulletPrefab);
            bulletLeft.transform.position = FirePointLeft.transform.position;    // 플레이어 위치로.
            GameObject bulletRight = Instantiate(bulletPrefab);
            bulletRight.transform.position = FirePointRight.transform.position;    // 플레이어 위치로.
        } 
    }
}
