using UnityEngine;
using TMPro;

public class StageNumberView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI StageNumberText;

    public void OnStageNumberChanged(StageNumberChanged evt)
    {
        StageNumberText.text = $"Stage {evt.NewStage}";
    }

    private void OnEnable()
    {
        EventManager.AddListener<StageNumberChanged>(OnStageNumberChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<StageNumberChanged>(OnStageNumberChanged);
    }
}
