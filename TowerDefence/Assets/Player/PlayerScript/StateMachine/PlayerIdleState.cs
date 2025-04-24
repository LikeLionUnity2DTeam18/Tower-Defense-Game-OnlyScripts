using UnityEngine;

/// <summary>
/// Idle 상태
/// AttackReady를 상속해 주변에 적이 있으면 Attack로 전환
/// </summary>
public class PlayerIdleState : PlayerAttackReadyState
{
    public PlayerIdleState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
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
