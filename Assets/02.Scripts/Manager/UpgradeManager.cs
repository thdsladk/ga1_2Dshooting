using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // static(정적)
    private static UpgradeManager _instance;
    public static UpgradeManager Instance => _instance;

    [SerializeField] private Upgrade[] _upgrades;
    public Upgrade[] Upgrades => _upgrades;

    [SerializeField] private UI_Upgrade[] _uiUpgrades;

    private const string UpgradeSaveDataKey = "UpgradeSaveData";

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
        // 시작할때 불러오고
        Load();

        RefreshUI();
    }

    public void LevelUp(int index)
    {
        // Todo : 묻지말고 시켜라 ! 
        // 골드 매니저에게 돈이 있는지 물어보고 돈이 있다면 차감후 업그레이드 

        Upgrade upgrade = _upgrades[index];
        // 매니저 간의 협력 
        if (ScoreManager.Instance.Score < upgrade.Cost)
        {
            return;
        }

        ScoreManager.Instance.SpendScore(upgrade.Cost);

        _upgrades[index].LevelUp();
        // 레벨업 하면 저장
        Save();

        RefreshUI();
    }

    // UI 갱신
    private void RefreshUI()
    {
        foreach (UI_Upgrade uiUpgrade in _uiUpgrades)
        {
            uiUpgrade.Refresh();
        }
    }

    private void Save()
    {
        // 데이터 저장은 유의미한 정보만 저장을 한다. 
        // 그래서 레벨만 저장한다.
        // 데이터를 분산해서 저장하면 오버헤드와 데이터 접근에서 캐시 미스가 생길수 있다.
        // Todo : 암호화 복호화 

        UpgradeSaveData saveData = new UpgradeSaveData(_upgrades.Length);
        for (int i = 0; i < _upgrades.Length; i++)
        {
            saveData.Name[i] = _upgrades[i].Name;
            saveData.Level[i] = _upgrades[i].Level;
        }
        // 게임 데이터 보면 확장자가 게임별로 다 다르다.
        // JSON 포맷으로 문자열 변환으로
        // 키와 밸류 형태로 저장한 형태

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(UpgradeSaveDataKey, json);
        PlayerPrefs.Save();
        Debug.Log($"{json} 저장 완료");
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(UpgradeSaveDataKey) == false) return;

        string json = PlayerPrefs.GetString(UpgradeSaveDataKey);
        UpgradeSaveData saveData = JsonUtility.FromJson<UpgradeSaveData>(json);

        for (int i = 0; i < _upgrades.Length; i++)
        {
            _upgrades[i].SetLevel(saveData.Level[i]);
        }
    }
}