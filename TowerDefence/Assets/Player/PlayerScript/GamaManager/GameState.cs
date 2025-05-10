using UnityEngine;

public class GameState
{
    protected GameManager game;
    protected GameStateMachine stateMachine;
    protected PlayerInputHandler input;
    protected float stateTimer;

    public GameState(GameStateMachine stateMachine, PlayerInputHandler input, GameManager game)
    {
        this.stateMachine = stateMachine;
        this.input = input;
        this.game = game;
    }



    public virtual void Enter()
    {
        // 일시정지버튼 리스너 등록
        EventManager.AddListener<GamePaused>(OnPause);
        Debug.Log("상태 진입");
    }

    public virtual void Exit()
    {
        // 일시정지버튼 리스너 해제
        EventManager.RemoveListener<GamePaused>(OnPause);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public void OnPause(GamePaused evt)
    {
        if (evt.IsPaused)
            stateMachine.Pause();
        else
            stateMachine.Resume();
    }

}
