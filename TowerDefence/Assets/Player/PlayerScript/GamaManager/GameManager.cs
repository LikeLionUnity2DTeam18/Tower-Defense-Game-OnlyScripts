using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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
    public List<StageData> StageDataList => stageDataSO.data;
    public int CurrentStage {  get; private set; }
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

        Input.OnInventoryPressed += ToggleInventory;
        EventManager.AddListener<ToggleMenu>(OnToggleMenu);
    }

    private void OnDestroy()
    {
        Input.OnInventoryPressed -= ToggleInventory;
        EventManager.RemoveListener<ToggleMenu>(OnToggleMenu);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();


        if(UnityEngine.Input.GetKeyDown(KeyCode.Space))
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

    public void ProceedStage() => CurrentStage++;

    public void ToggleInventory()
    {
        EventManager.Trigger(new ToggleInventory());
    }

    private void OnToggleMenu(ToggleMenu _)
    {
        if(isMenuOn)
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

}
