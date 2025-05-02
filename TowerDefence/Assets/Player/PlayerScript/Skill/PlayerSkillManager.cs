using UnityEngine;

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



}
