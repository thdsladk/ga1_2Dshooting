using UnityEngine;
using UnityEngine.UI;

public class UI_AutoButton : MonoBehaviour
{
    // 버튼을 클릭하면 토글하고 싶다.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격
    private Image _myImage;

    [Header("on/off스프라이트")]
    [SerializeField] private Sprite _onSprite;

    [SerializeField] private Sprite _offSprite;

    private bool _autoMode = false;
    private Player _player;


    private void Start()
    {
        _player = GameObject.FindAnyObjectByType<Player>();
        _myImage = GetComponent<Image>();


        AutoToggle();
    }

    private void Update()
    {
    }

    public void AutoToggle()
    {
        _autoMode = !_autoMode;
        _player.GetComponent<PlayerFire>().SetAutoMode(_autoMode);
        _player.GetComponent<PlayerMove>().enabled = !_autoMode;
        _player.GetComponent<PlayerAutoMove>().enabled = _autoMode;

        _myImage.sprite = (_autoMode) ? _onSprite : _offSprite;
    }
    // todo : 버튼 클릭할 때 애니메이션 주기 + 사운드 주기 
    // 애니메이션 컴포넌트 : UI_ButtonClick

    // 애니메이션 : 코드로 구현 약간 커졌다가 작아지기...
    // 사운드 : 일레븐랩스에서 버튼 클릭 공용 사운드 만들어서 적용
}