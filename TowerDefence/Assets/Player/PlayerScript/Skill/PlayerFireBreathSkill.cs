using UnityEngine;

public class PlayerFireBreathSkill : Skill
{
    [SerializeField] float duration;
    [SerializeField] float damage;
    [SerializeField] float damageInterval;
    [SerializeField] float length;

    protected override void Start()
    {
        base.Start();
        hasPreviewState = false;
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        CreateSkillObject();
        player.stateMachine.ChangeState(player.breathState);
    }

    private void CreateSkillObject()
    {
        var go = PoolManager.Instance.Get(skillPrefab);
        var obj = go.GetComponent<FireBreathController>();
        obj.SetFireBreath(skillCenterPosition, duration, damage, damageInterval, length);
    }
}
