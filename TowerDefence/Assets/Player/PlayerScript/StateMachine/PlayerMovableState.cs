using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 좌클릭으로 이동이 가능한 상태 
/// </summary>
public class PlayerMovableState : PlayerState
{
    private LayerMask cannotMoveMask = LayerMask.GetMask("Enemy", "Beacon");
    Vector2 direction;

    public PlayerMovableState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();


        input.OnLeftClick += SetDestination;
    }

    public override void Exit()
    {
        base.Exit();
        input.OnLeftClick -= SetDestination;
    }

    public override void Update()
    {
        base.Update();

        HandleMove();

    }

    private void HandleMove()
    {
        if (player.HasDestination)
        {
            direction = (player.Destination - (Vector2)player.transform.position).normalized;
            rb.linearVelocity = direction * player.MoveSpeed;


            if (IsSamePosition(player.Destination, player.transform.position))
            {
                rb.linearVelocity = Vector2.zero;
                player.ResetDestination();
            }
        }
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


        // UI인 경우
        // if(UI) return;

        // 몬스터,타워 등을 클릭한 경우
        RaycastHit2D hit = Physics2D.Raycast(destination, Vector2.zero, 0f, cannotMoveMask);
        if(hit.collider != null)
        {
            var go = hit.collider.gameObject;

            if(go.TryGetComponent<EnemyController>(out var monster))
            {
                // 몬스터 정보 표시
                //ex) UI.displayinfo(monster);
                return;
            }
            if (go.TryGetComponent<Tower>(out var tower))
            {
                //타워 정보 표시
                //ex) UI.displayinfo(tower);
                return;
            }
            if (go.TryGetComponent<DraggableIcon>(out var icon))
            {
                return;
            }
        }

        player.SetDestination(destination);
    }
}
