using UnityEngine;
using TMPro;

public class TimeScaleView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText; //버튼 위에 표시할 텍스트

    private void Awake()
    {
        speedText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        EventManager.AddListener<SpeedChanged>(OnSpeedChanged); //구독
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<SpeedChanged>(OnSpeedChanged); //해제
    }

    private void OnSpeedChanged(SpeedChanged evt)
    {
        Debug.Log((int)evt.NewSpeed);
       speedText.text = $"{(int)evt.NewSpeed}x"; //배속 글씨 변경
    }
}
