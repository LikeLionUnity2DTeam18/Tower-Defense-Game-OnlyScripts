using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovableState : PlayerState
{

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
        player.SetDestination(destination);
    }
}
