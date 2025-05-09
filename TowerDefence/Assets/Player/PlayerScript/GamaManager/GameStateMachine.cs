using UnityEditor;
using UnityEngine;

public class GameStateMachine
{

    public GameState currentState { get; private set; }
    public GameState prevState { get; private set; }
    private GameManager game;

    public void Initialize(GameState state)
    {
        currentState = state;
        currentState.Enter();
        game = GameManager.Instance;
    }

    public void ChangeState(GameState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void Update()
    {
        currentState.Update();
    }

    public void Pause()
    {
        if (currentState is GamePauseState) return;

        prevState = currentState;
        currentState = game.PauseState;
        currentState.Enter();
    }

    public void Resume()
    {
        if (currentState is not GamePauseState) return;

        currentState.Exit();
        currentState = prevState;
        prevState = null;
    }

}
