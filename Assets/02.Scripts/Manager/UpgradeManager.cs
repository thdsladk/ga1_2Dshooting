using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // static(정적)
    private static UpgradeManager _instance;
    public static UpgradeManager Instance => _instance;

    [SerializeField] private Upgrade[] _upgrades;
    public Upgrade[] Upgrades => _upgrades;

    [SerializeField] private UI_Upgrade[] _uiUpgrades;

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
        RefreshUI();
    }

    public void LevelUp(int index)
    {
        // 골드 매니저에게 돈이 있는지 물어보고 돈이 있다면 차감후 업그레이드 

        Upgrade upgrade = _upgrades[index];
        // 매니저 간의 협력 
        if (ScoreManager.Instance.Score < upgrade.Cost)
        {
            return;
        }

        ScoreManager.Instance.SpendScore(upgrade.Cost);

        _upgrades[index].LevelUp();
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
}