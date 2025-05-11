using UnityEngine;

public class GoldController : MonoBehaviour
{
    [SerializeField] private GoldView view;
    private GoldModel model;
    public GoldModel Model => model;//eye에서 골드 모델 접근하기 위함.
    private void Start()
    {
        model = new GoldModel();
        model.SetGold(100); //기본 소지 골드
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) //Addgold 테스트용
        {
            model.AddGold(10);
        }
        if(Input.GetKeyDown(KeyCode.H)) //SpendGold 테스트용
        {
            model.SpendGold(20);
        }
        if(Input.GetKeyDown(KeyCode.J)) //몬스터 죽으면 그 자리에 골드 표시
        {
            EventManager.Trigger<MonsterDied>(new MonsterDied(100,new Vector3(1,1,1)));
        }
    }
    public void OnMonsterDied(MonsterDied evt)
    {
        model.AddGold(evt.RewardGold);
    }

    public void OnGoldSpended(GoldSpended evt)
    {
        model.SpendGold(evt.Amount);
    }
    
    private void OnEnable()
    {
        EventManager.AddListener<MonsterDied>(OnMonsterDied);
        EventManager.AddListener<GoldSpended>(OnGoldSpended);
    }
    private void OnDisable()
    {
        EventManager.RemoveListener<MonsterDied>(OnMonsterDied);
        EventManager.RemoveListener<GoldSpended>(OnGoldSpended);
    }
}
