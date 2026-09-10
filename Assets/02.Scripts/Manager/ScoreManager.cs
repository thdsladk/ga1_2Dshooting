using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // static(정적)
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    // 관리 : 특정 데이터에 대한 무결성과 생성, 읽기, 수정, 삭제 등과 관련된 게임 로직

    private int _bestScore = 0;
    private int _currentScore = 0;

    private string SaveKey = "BestScore";

    // UI 책임 추가         //TextMeshProUGUI UI용 Canvas안에서 생성되는 애
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 입력 : Input 
        // 저장 / 불러오기 : PlayerPrefs
        if (PlayerPrefs.HasKey(SaveKey))
        {
            _bestScore = PlayerPrefs.GetInt(SaveKey);
        }

        // 이 방식으로 해도 데이터가 있으면 가져온다. 
        //_bestScore = PlayerPrefs.GetInt(SaveKey,0);

        Refresh();
    }


    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;


            /// 저장 : PlayerPrefs.Set~ 시리즈를 사용해서 int/float/string을 저장 가능
            /// 내 컴퓨터 어딘가에 저장...
            PlayerPrefs.SetInt(SaveKey, +_bestScore);
            // 명시적으로 호출 해야 저장이 보장된다. ( 갑자기 종료되면 날라갈수 있어서 명시적으로 )
            PlayerPrefs.Save();
        }

        // UI 갱신 
        Refresh();
    }

    private void Update()
    {
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"BestScore: {_bestScore}";
        _currentScoreTextUI.text = $"Score: {_currentScore}";
    }
}