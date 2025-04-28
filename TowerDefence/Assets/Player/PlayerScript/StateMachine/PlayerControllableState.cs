using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 플레이어의 모든 조작이 가능한 상태
/// AttackReady(Idle, Move의 부모), Attack에서 상속
/// </summary>
public class PlayerControllableState : PlayerState
{
    private System.Action qSkillLamda;

    public PlayerControllableState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        qSkillLamda = () => player.UseSkill(player.skill.wskill);

        input.OnLeftClick += SetDestination;
        input.OnSkillQPressed += qSkillLamda;
    }

    public override void Exit()
    {
        base.Exit();
        input.OnLeftClick -= SetDestination;
        input.OnSkillQPressed -= qSkillLamda;
    }

    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 마우스 왼쪽 클릭 시 플레이어 캐릭터의 목적지 설정
    /// 나중에 UI클릭시 예외처리 필요할듯
    /// </summary>
    protected void SetDestination()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log($"마우스 클릭 : {destination}");
        player.SetDestination(destination);
    }



}
