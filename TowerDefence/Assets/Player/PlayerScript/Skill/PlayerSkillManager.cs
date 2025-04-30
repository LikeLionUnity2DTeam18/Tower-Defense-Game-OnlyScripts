using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{

    
    public PlayerBindShotSkill qskill { get; private set; }
    
    public WallSkill wskill { get; private set; }

    public PlayerFireBreathSkill rskill { get; private set; }
    

    private void Awake()
    {

    }

    private void Start()
    {
        qskill = GetComponent<PlayerBindShotSkill>();
        wskill = GetComponent<WallSkill>();
        rskill = GetComponent<PlayerFireBreathSkill>();
    }



}
