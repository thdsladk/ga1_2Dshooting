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


    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
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