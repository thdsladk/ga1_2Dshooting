using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    public Transform FirePointLeft;
    public Transform FirePointRight;
    public Transform FirePointLeft_Sub;
    public Transform FirePointRight_Sub;

    public float CoolDown = 3f;
    [SerializeField]
    private float _fireTimer = 0f;

    private bool _isAutoFireMode = false;
    
     private void Start()
     {
         _fireTimer = CoolDown;
     }

     private void Update()
    {


        SwitchAutoFire();
        if (_fireTimer > 0f)
        {
            _fireTimer -= Time.deltaTime;
        }
        else
        {
            FireBullet(_isAutoFireMode);
        }
    }
     
    private void FireBullet(bool isAuto = false)
    {
        // 1. 스페이스 바를 누르면 
        if (isAuto == true || Input.GetKeyDown(KeyCode.Space))
        {
            // 2. 총알 프리펩 생성 
            // Instantiate는 프리팹을 복사해서 (Monobehavior를 상속받는) 게임 오브젝트를 생성하는 씬에 넣어주는 기능
            GameObject bulletLeft = Instantiate(BulletPrefab);
            bulletLeft.transform.position = FirePointLeft.transform.position;    // 플레이어 위치로.
            GameObject bulletRight = Instantiate(BulletPrefab);
            bulletRight.transform.position = FirePointRight.transform.position;    // 플레이어 위치로.
            
            // 보조 총알
            GameObject subBulletLeft = Instantiate(SubBulletPrefab);
            subBulletLeft.transform.position = FirePointLeft_Sub.transform.position;    // 플레이어 위치로.
            GameObject subBulletRight = Instantiate(SubBulletPrefab);
            subBulletRight.transform.position = FirePointRight_Sub.transform.position;    // 플레이어 위치로.
            
            // 발사후 초기화
            _fireTimer = CoolDown;
        } 
    }
    
    private void SwitchAutoFire()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoFireMode = !_isAutoFireMode;
        }
    }
}
