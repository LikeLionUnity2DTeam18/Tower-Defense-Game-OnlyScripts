using UnityEngine;

public class BaseTowerController : MonoBehaviour
{
    private BaseTowerModel model;

    void Start()
    {
        model = new BaseTowerModel(200); // 체력 초기화
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(25);
        }
    }

    public void TakeDamage(int amount)
    {
        model.TakeDamage(amount);
        GetComponent<BaseTowerFX>().Shake();

        if(model.IsDead())
        {
            Debug.Log("기지 파괴!!!");
            //GameManager.Instance.GameOver();
        }
    }
}
