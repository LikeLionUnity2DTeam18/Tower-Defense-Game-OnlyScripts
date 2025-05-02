using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    TimeScaleModel model;

    private void Awake()
    {
        model = new TimeScaleModel();
    }
    public void OnClickSpeedButton()
    {
        model.ChangeSpeed();
    }

}
