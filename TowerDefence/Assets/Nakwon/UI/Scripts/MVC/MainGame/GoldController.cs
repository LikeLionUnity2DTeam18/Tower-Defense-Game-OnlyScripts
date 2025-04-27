using UnityEngine;

public class GoldController : MonoBehaviour
{
    [SerializeField] GoldView view;
    private GoldModel model;

    void Start()
    {
        model = new GoldModel();
        model.SetGold(100);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            model.AddGold(10);
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            model.SpendGold(20);
        }
    }

}
