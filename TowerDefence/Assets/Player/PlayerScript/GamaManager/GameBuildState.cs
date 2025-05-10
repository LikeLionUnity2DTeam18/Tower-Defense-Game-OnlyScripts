using UnityEngine;

public class GameBuildState : GameState
{
    public GameBuildState(GameStateMachine stateMachine, PlayerInputHandler input, GameManager game) : base(stateMachine, input, game)
    {
    }

    public override void Enter()
    {
        base.Enter();
        EventManager.AddListener<GameStarted>(OnStartButtonClicked);

        Debug.Log("타워 건설 페이즈 진입");
    }

    public override void Exit()
    {
        base.Exit();
        EventManager.RemoveListener<GameStarted>(OnStartButtonClicked);
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnStartButtonClicked(GameStarted _)
    {
        stateMachine.ChangeState(game.WaveState);
    }
}
