using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{


    public Skill qskill { get; private set; }

    private void Awake()
    {

    }

    private void Start()
    {
        qskill = GetComponent<Skill>();
    }



}
