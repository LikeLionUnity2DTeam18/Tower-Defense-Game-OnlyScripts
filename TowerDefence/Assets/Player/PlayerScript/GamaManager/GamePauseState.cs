using UnityEngine;

public class GamePauseState : GameState
{
    private int prevScale;
    public GamePauseState(GameStateMachine stateMachine, PlayerInputHandler input, GameManager game) : base(stateMachine, input, game)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("일시정지");

        prevScale = (int) Time.timeScale;
        // Unity의 시간 배속을 반영
        Time.timeScale = 0;
        
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("일시정지 해제");
        Time.timeScale = prevScale;
    }

    public override void Update()
    {
        base.Update();
    }
}
