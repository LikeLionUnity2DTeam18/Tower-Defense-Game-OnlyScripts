using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{

    [Header("스킬 Q")]
    public PlayerBindShotSkill qskill { get; private set; }
    [Header("스킬 W")]
    public WallSkill wskill { get; private set; }

    private void Awake()
    {

    }

    private void Start()
    {
        qskill = GetComponent<PlayerBindShotSkill>();
        wskill = GetComponent<WallSkill>();
    }



}
