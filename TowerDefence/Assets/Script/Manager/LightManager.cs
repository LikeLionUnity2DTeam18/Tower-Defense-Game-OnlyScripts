using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light2D light2D;

    private void Awake()
    {
        EventManager.AddListener<StageChangeEvent>(OnStageChange);
    }

    private void OnStageChange(StageChangeEvent evt)
    {
        ActiveSwitch(evt);
    }

    public void ActiveSwitch(StageChangeEvent evt)
    {
        switch (evt.EventType)
        {
            case StageChangeEventType.Start:
                DOTween.To(() => light2D.intensity,x => light2D.intensity = x,0.3f,1f);
                break;
            case StageChangeEventType.End:
                DOTween.To(() => light2D.intensity,x => light2D.intensity = x,1f,1f);
                break;
        }
    }
}
