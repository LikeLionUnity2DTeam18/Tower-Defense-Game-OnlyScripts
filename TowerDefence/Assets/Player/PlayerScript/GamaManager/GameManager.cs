using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerInputHandler Input { get; private set; }
    public GameStateMachine StateMachine { get; private set; }
    public GameWaveState WaveState { get; private set; }
    public GameBuildState BuildState { get; private set; }
    public GamePauseState PauseState { get; private set; }

    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private StageDataSO stageDataSO;
    [SerializeField] private GameObject menuObj;
    [SerializeField] private GameObject gameOverPannel; //게임 오버 패널
    public List<StageData> StageDataList => stageDataSO.data;
    private PlayerController player;
    public int CurrentStage { get; private set; }
    private bool isMenuOn;


    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Input = GetComponent<PlayerInputHandler>();
        StateMachine = new GameStateMachine();
    }

    void Start()
    {
        BuildState = new GameBuildState(StateMachine, Input, this);
        WaveState = new GameWaveState(StateMachine, Input, this);
        PauseState = new GamePauseState(StateMachine, Input, this);
        CurrentStage = 1;
        StateMachine.Initialize(BuildState);
        isMenuOn = false;
        menuObj.SetActive(false);

        Input.OnInventoryPressed += ToggleInventory;
        EventManager.AddListener<ToggleMenu>(OnToggleMenu);
        EventManager.AddListener<MenuButtonClicked>(OnMenuButtonClicked);

        player = PlayerManager.Instance.Player;


        /// 경고 처리용
        var allGOs = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var go in allGOs)
        {
            var comps = go.GetComponents<MonoBehaviour>();
            for (int i = 0; i < comps.Length; i++)
            {
                if (comps[i] == null)
                    Debug.Log($"Missing script on [{go.name}]", go);
            }
        }

    }

    private void OnDestroy()
    {
        Input.OnInventoryPressed -= ToggleInventory;
        EventManager.RemoveListener<ToggleMenu>(OnToggleMenu);
        EventManager.RemoveListener<MenuButtonClicked>(OnMenuButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();


        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.Trigger(new ToggleMenu());
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.Trigger(new StartButtonClick());
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            EventManager.Trigger(new GamePaused(true));
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.O))
        {
            EventManager.Trigger(new GamePaused(false));
        }
    }

    public void ProceedStage()
    {
        CurrentStage++;
        EventManager.Trigger<StageNumberChanged>(new StageNumberChanged(CurrentStage));
        if (CurrentStage >= StageDataList.Count)
        {
            Debug.Log("게임 끝");
        }
    }

    public void ToggleInventory()
    {
        EventManager.Trigger(new ToggleInventory());
    }

    private void OnToggleMenu(ToggleMenu _)
    {
        if (isMenuOn)
        {
            //메뉴닫기
            isMenuOn = false;
            StateMachine.CloseMenu();
            menuObj.SetActive(false);
        }
        else
        {
            //메뉴열기
            isMenuOn = true;
            StateMachine.OpenMenu();
            menuObj.SetActive(true);
        }
    }

    private void OnMenuButtonClicked(MenuButtonClicked evt)
    {
        switch (evt.type)
        {
            case MenuButtonTypes.Start:
                OnToggleMenu(new ToggleMenu());
                break;
            case MenuButtonTypes.Option:
                player.Skill.SetSmartCastingAll(true);
                break;
            case MenuButtonTypes.Exit:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
                break;

        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPannel != null)
            gameOverPannel.SetActive(true);
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickReturnToMain()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }

}
