using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerTrail : MonoBehaviour
{
    [SerializeField] private GameObject _leftTrailPrefab;
    [SerializeField] private GameObject _rightTrailPrefab;
    private const float _defalutScake = 1f;
    
    private void Start()
    {
        
    }

     private void Update()
    {
        
    }

    public void Wing()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Approximately(horizontal, 0))
        {
            
        }
        else if(horizontal > 0)
        {
            // right
        }
        else if (horizontal < 0)
        {
            // left
        }
        
    }
}
