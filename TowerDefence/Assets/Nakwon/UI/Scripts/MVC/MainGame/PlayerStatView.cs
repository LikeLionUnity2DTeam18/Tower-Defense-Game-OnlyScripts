using TMPro;
using UnityEngine;

public class PlayerStatView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackDamageText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI attackRangeText;
    [SerializeField] private TextMeshProUGUI skillPowerText;
    [SerializeField] private TextMeshProUGUI moveSpeedText;

    private void Start()
    {
        UpdateAllStats();
    }
    private void OnEnable()
    {
        EventManager.AddListener<PlayerStatChanged>(OnStatsChanged);
    }
    private void OnDisable()
    {
        EventManager.RemoveListener<PlayerStatChanged>(OnStatsChanged);
    }

    private void OnStatsChanged(PlayerStatChanged evt)
    {
        string label = "";
        switch (evt.type)
        {
            case PlayerStatTypes.baseAttackDamage:
                label = "공격력".PadRight(6);
                attackDamageText.text = $"{label} : {evt.value}";
                break;
            case PlayerStatTypes.baseattackSpeed:
                label = "공격 속도".PadRight(6);
                attackSpeedText.text = $"{label} : {evt.value}";
                break;
            case PlayerStatTypes.baseattackRange:
                label = "사거리".PadRight(6);
                attackRangeText.text = $"{label} : {evt.value}";
                break;
            case PlayerStatTypes.skillPower:
                label = "스킬 공격력".PadRight(6);
                skillPowerText.text = $"{label} : {evt.value}";
                break;
            case PlayerStatTypes.moveSpeed:
                label = "이동속도".PadRight(6);
                moveSpeedText.text = $"{label} : {evt.value}";
                break;
            default:
                break;
        }
    }

    private void UpdateAllStats()
    {
        var stats = PlayerManager.Instance.Player.stats;

        foreach (PlayerStatTypes type in System.Enum.GetValues(typeof(PlayerStatTypes)))
        {
            float value = stats.GetStatValue(type);
            OnStatsChanged(new PlayerStatChanged(type, value));
        }
    }
}
