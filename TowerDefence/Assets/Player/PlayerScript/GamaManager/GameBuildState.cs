using UnityEngine;

public class GameBuildState : GameState
{
    public GameBuildState(GameStateMachine stateMachine, PlayerInputHandler input, GameManager game) : base(stateMachine, input, game)
    {
    }

    public override void Enter()
    {
        base.Enter();
        EventManager.AddListener<StartButtonClick>(OnStartButtonClicked);
        EventManager.Trigger(new StageChangeEvent(StageChangeEventType.End, game.CurrentStage));

        Debug.Log("타워 건설 페이즈 진입");
    }

    public override void Exit()
    {
        base.Exit();
        EventManager.RemoveListener<StartButtonClick>(OnStartButtonClicked);
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnStartButtonClicked(StartButtonClick _)
    {
        stateMachine.ChangeState(game.WaveState);
    }
}
