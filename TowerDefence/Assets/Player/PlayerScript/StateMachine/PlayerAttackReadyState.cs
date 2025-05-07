/// <summary>
/// 주변에 적이 있는지 탐색하고, 있다면 공격으로 전환할 수 있는 상태
/// Idle, Move에서 상속
/// </summary>
public class PlayerAttackReadyState : PlayerCanUseSkillState
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

        if (IsBaseAttackReady())
        {
            var nearestTarget = FindTargetInRange(player.BaseAttackRange); // 기본공격 사정거리 안의 대상 확인
            if (nearestTarget != null) // 적이 있다면
            {
                stateMachine.ChangeState(player.AttackState);
            }

            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    stateMachine.ChangeState(player.attackState);
            //}
        }
    }



    protected virtual bool IsBaseAttackReady()
    {
        return player.BaseAttackTimer <= 0f;
    }
}
