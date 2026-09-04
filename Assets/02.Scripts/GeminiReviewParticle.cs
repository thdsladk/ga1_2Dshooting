using UnityEngine;

public class GeminiReviewParticle : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        Debug.Log($"Player Position: {transform.position} {transform.rotation}");
    }
}