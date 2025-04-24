using UnityEngine;

/// <summary>
/// 주변에 적이 있는지 탐색하고, 있다면 공격으로 전환할 수 있는 상태
/// Idle, Move에서 상속
/// </summary>
public class PlayerAttackReadyState : PlayerControllableState
{
    public PlayerAttackReadyState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
