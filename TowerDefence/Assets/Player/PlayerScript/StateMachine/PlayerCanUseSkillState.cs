using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 플레이어의 모든 조작이 가능한 상태
/// AttackReady(Idle, Move의 부모), Attack에서 상속
/// </summary>
public class PlayerCanUseSkillState : PlayerMovableState
{
    private System.Action qSkillLamda;
    private System.Action wSkillLamda;
    private System.Action eSkillLamda;
    private System.Action rSkillLamda;

    public PlayerCanUseSkillState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        qSkillLamda = () => player.UseSkill(player.Skill.qskill);
        wSkillLamda = () => player.UseSkill(player.Skill.wskill);
        eSkillLamda = () => player.UseSkill(player.Skill.eskill);
        rSkillLamda = () => player.UseSkill(player.Skill.rskill);

        input.OnSkillQPressed += qSkillLamda;
        input.OnSkillWPressed += wSkillLamda;
        input.OnSkillEPressed += eSkillLamda;
        input.OnSkillRPressed += rSkillLamda;
    }

    public override void Exit()
    {
        base.Exit();
        input.OnSkillQPressed -= qSkillLamda;
        input.OnSkillWPressed -= wSkillLamda;
        input.OnSkillEPressed -= eSkillLamda;
        input.OnSkillRPressed -= rSkillLamda;
    }

    public override void Update()
    {
        base.Update();
    }



}
