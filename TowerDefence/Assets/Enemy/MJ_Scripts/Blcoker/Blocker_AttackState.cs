using System.Collections;
using UnityEngine;

public class Blocker_AttackState : EnemyState
{
    public Blocker_AttackState(EnemyController enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) { }

    public override void Enter()
    {
        stateTimer = 1f;
        if (enemy.MoveDir.y > 0)
        {
            enemy.Animator.Play("Idle_back");
        }
        else
        {
            enemy.Animator.Play("Idle_front");
        }
        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
        enemy.StartCoroutine(AttackEffect());
    }

    private IEnumerator AttackEffect()
    {
        Vector3 originalPos = enemy.transform.position;

        // 살짝 앞으로 튕기기
        Vector3 attackOffset = new Vector3(0.1f * enemy.MoveDir.x, 0.1f * enemy.MoveDir.y, 0f);
        enemy.transform.position += attackOffset;

        // 이펙트나 사운드 재생 가능!
        // 예: PlaySound("AttackSFX"); or Instantiate(effectPrefab, enemy.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.1f);

        // 원래 위치로 되돌리기
        enemy.transform.position = originalPos;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(new Blocker_IdleState(enemy, stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}