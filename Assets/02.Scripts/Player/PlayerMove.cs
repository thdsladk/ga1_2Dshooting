using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리
     [SerializeField]
     public float moveSpeed = 5f;

     public Camera cam;
     [SerializeField]
     public float padding = 1f; // 화면 끝에서 얼마나 떨어뜨릴지 (뷰포트 단위)

     private void Start()
     {
         if (cam == null)
         { 
             cam = Camera.main;
         }
     }
     // 매 프레임마다 실행 된다. 
     // 초당 프레임 
     private void Update()
    {
        // 1. 키보드 입력 
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("정면 방향 키를 누르는 중입니다. ");
        }
        if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            
        }
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            
        }

        // 속도 조절
        if (Input.GetKeyDown(KeyCode.E))
        {
            moveSpeed = Mathf.Min(moveSpeed+1f,20f);
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            moveSpeed = Mathf.Max(moveSpeed-1f,5f);
        }
        
        
        // 2. 키보드 입력에 따라 방향 계산
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0f);
        // 즉시 입력값을 -1 ,1로 나올려면 GetAxisRaw를 쓴다. 점진적으로 가속은 GetAxis
        //Debug.Log($"{direction.x},{direction.y}으로 이동중");

        // 3. 방향과 속도에 따라 이동
        Vector3 normalizedSpeed = (direction ).normalized;
        
        transform.Translate(normalizedSpeed * Time.deltaTime * moveSpeed );

        // 반대편으로 이동
        if (transform.position.x < (-2f + padding))
        {
            transform.position = new Vector3(2f - padding, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > (2f - padding))
        {
            transform.position = new Vector3(-2f + padding, transform.position.y, transform.position.z);
        }
        
        // 범위 제한
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, (-2f + padding), (2f -padding)),
            Mathf.Clamp(transform.position.y, (-5f + padding), (0f-padding)));
}
