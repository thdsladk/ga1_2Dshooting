using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerTrail : MonoBehaviour
{
    [SerializeField] private GameObject _leftTrailPrefab;
    [SerializeField] private GameObject _rightTrailPrefab;
    private const float _defalutScale = 1f;

    [SerializeField] private TrailRenderer _LeftTrailRenderer;
    [SerializeField] private TrailRenderer _RightTrailRenderer;

    public void Wing()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Approximately(horizontal, 0))
        {
        }
        else if (horizontal > 0)
        {
            // right
        }
        else if (horizontal < 0)
        {
            // left
        }
    }

    public void CalculateTrail()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 normalizedDirection = new Vector2(h, v).normalized;

        if (normalizedDirection.y > 0)
        {
            if (_LeftTrailRenderer != null && _RightTrailRenderer != null)
            {
                _LeftTrailRenderer.emitting = true;
                _RightTrailRenderer.emitting = true;
            }
        }
        else
        {
            if (_LeftTrailRenderer != null && _RightTrailRenderer != null)
            {
                _LeftTrailRenderer.emitting = false;
                _RightTrailRenderer.emitting = false;
            }
        }
    }
}