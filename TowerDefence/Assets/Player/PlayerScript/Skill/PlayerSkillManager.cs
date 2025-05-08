using UnityEngine;

/// <summary>
/// 이름은 스킬매니저이지만 플레이어 자식으로 붙일거라 따로 싱글톤은 필요없음
/// </summary>
public class PlayerSkillManager : MonoBehaviour
{

    
    public PlayerBindShotSkill qskill { get; private set; }
    
    public PlayerWallSkill wskill { get; private set; }

    public PlayerPowerUpSkill eskill { get; private set; }

    public PlayerFireBreathSkill rskill { get; private set; }
    

    private void Awake()
    {

    }

    private void Start()
    {
        qskill = GetComponent<PlayerBindShotSkill>();
        wskill = GetComponent<PlayerWallSkill>();
        eskill = GetComponent<PlayerPowerUpSkill>();
        rskill = GetComponent<PlayerFireBreathSkill>();
    }

    public void SetSmartCastingAll(bool smartCasting)
    {
        qskill.SetSmartCasting(smartCasting);
        wskill.SetSmartCasting(smartCasting);
        eskill.SetSmartCasting(smartCasting);
        rskill.SetSmartCasting(smartCasting);
    }

}
