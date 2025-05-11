using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Image image;
   [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite startSprite;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        EventManager.AddListener<StartButtonClick>(OnStartButtonClick);
        EventManager.AddListener<StageChangeEvent>(OnStageChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<StartButtonClick>(OnStartButtonClick);
        EventManager.RemoveListener<StageChangeEvent>(OnStageChange);
    }

    public void OnStartButtonClick(StartButtonClick _)
    {
        OnClick();
    }

    public void OnClick()
    {
        if(GameManager.Instance.StateMachine.currentState is GamePauseState)
        {
            EventManager.Trigger(new GamePaused(false));
            image.sprite = pauseSprite;
        }
        else if (GameManager.Instance.StateMachine.currentState is GameWaveState)
        {
            EventManager.Trigger(new GamePaused(true));
            image.sprite = startSprite;
        }
        else if(GameManager.Instance.StateMachine.currentState is GameBuildState)
        {
            EventManager.Trigger(new GameStarted());
            image.sprite = pauseSprite;
        }
    }

    public void OnStageChange(StageChangeEvent evt)
    {
        if (evt.EventType == StageChangeEventType.Start)
        {
            image.sprite = pauseSprite;
        }
        else if (evt.EventType == StageChangeEventType.SpawnEnd)
            return;
        else
        {
            image.sprite = startSprite;
        }
    }
}
