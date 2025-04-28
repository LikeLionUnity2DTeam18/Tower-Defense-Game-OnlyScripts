using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{


    public WallSkill qskill { get; private set; }

    private void Awake()
    {

    }

    private void Start()
    {
        qskill = GetComponent<WallSkill>();
    }



}
