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
    private System.Action rSkillLamda;

    public PlayerCanUseSkillState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        qSkillLamda = () => player.UseSkill(player.skill.qskill);
        wSkillLamda = () => player.UseSkill(player.skill.wskill);
        rSkillLamda = () => player.UseSkill(player.skill.rskill);

        input.OnSkillQPressed += qSkillLamda;
        input.OnSkillWPressed += wSkillLamda;
        input.OnSkillRPressed += rSkillLamda;
    }

    public override void Exit()
    {
        base.Exit();
        input.OnSkillQPressed -= qSkillLamda;
        input.OnSkillWPressed -= wSkillLamda;
        input.OnSkillRPressed -= rSkillLamda;
    }

    public override void Update()
    {
        base.Update();
    }



}
