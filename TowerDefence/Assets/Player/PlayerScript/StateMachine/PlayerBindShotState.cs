using UnityEngine;
public class PlayerBindShotState : PlayerState
{
    public PlayerBindShotState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = Vector2.zero;
        stateTimer = player.skill.qskill.CastingTime;
        //SetAnimationDirection(player.skill.qskill.previewDirection);
        Debug.Log(player.skill.qskill.previewDirection);
        player.SetLastDirection(player.skill.qskill.previewDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
