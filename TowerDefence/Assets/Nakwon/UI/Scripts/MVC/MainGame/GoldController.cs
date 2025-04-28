using UnityEngine;

public class GoldController : MonoBehaviour
{
    [SerializeField] private GoldView view;
    private GoldModel model;

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
    }
    public void OnMonsterDied(MonsterDied evt)
    {
        model.AddGold(evt.RewardGold);
    }
     private void OnEnable()
    {
        EventManager.AddListener<GoldChanged>(view.OnGoldChanged);
        EventManager.AddListener<MonsterDied>(OnMonsterDied);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<GoldChanged>(view.OnGoldChanged);
        EventManager.RemoveListener<MonsterDied>(OnMonsterDied);
    }

}
