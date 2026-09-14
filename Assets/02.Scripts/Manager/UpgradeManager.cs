using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // static(정적)
    private static UpgradeManager _instance;
    public static UpgradeManager Instance => _instance;

    [SerializeField] private Upgrade[] _upgrades;
    public Upgrade[] Upgrades => _upgrades;

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

    public void LevelUp(int index)
    {
    }
}