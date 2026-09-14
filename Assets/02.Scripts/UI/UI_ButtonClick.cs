using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonClick : MonoBehaviour
{
    private Button _button;
    private AudioSource _audioSource;

    [Header("클릭시 애니메이션")]
    [SerializeField] private AnimationCurve _bumpCurve;


    private float _scale = 1.0f;
    private bool _isBumping = false;
    private float _elapsedTime = 0.0f;
    private const float BumpDuration = 0.3f;
    private const float BumpScale = 1.1f;

    private void Awake()
    {

    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayButtonAnimation);
        _button.onClick.AddListener(PlaySound);
    }

    private void Update()
    {
        if (!_isBumping) return;

        // 1. 경과 시간 누적 
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > BumpDuration)
        {
            transform.localScale = Vector3.one;
            _isBumping = false;
            return;
        }

        // 2. 누적 시간과 애니메이션 커브에 따른 스케일 변경
        float time = _elapsedTime / BumpDuration; // 얼마나 지났는지 퍼센트 ( 0 ~ 1 )
        float curveValue = _bumpCurve.Evaluate(time); // 퍼센트에 따라 커브 애니메이션 값 추출
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * BumpScale, curveValue);
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    public void PlayButtonAnimation()
    {
        _isBumping = true;
        _elapsedTime = 0.0f;
    }
}