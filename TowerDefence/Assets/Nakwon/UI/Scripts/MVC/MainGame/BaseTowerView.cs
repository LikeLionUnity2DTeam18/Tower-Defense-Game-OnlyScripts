using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BaseTowerView : MonoBehaviour
{
    [SerializeField] private Slider healthSliderFore; // 즉시 반응
    [SerializeField] private Slider healthSliderFX; // 느리게 반응하는 애니메이션
    //[SerializeField] private Text healthText;

    private void Start()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();
        foreach (Slider s in sliders)
        {
            if (s.gameObject.name.Contains("FX"))
                healthSliderFX = s;

            else if (s.gameObject.name.Contains("Fore"))
                healthSliderFore = s;
        }
    }
    private void OnEnable()
    {
        EventManager.AddListener<BaseTowerHealthChanged>(OnHealthChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<BaseTowerHealthChanged>(OnHealthChanged);
    }

    private void OnHealthChanged(BaseTowerHealthChanged evt)
    {
        // MAX 설정
        healthSliderFore.maxValue = evt.MaxHealth;
        healthSliderFX.maxValue = evt.MaxHealth;

        // 실제 체력은 즉시 반영
        healthSliderFore.value = evt.CurrentHealth;

        // DOTween으로 부드럽게 줄이기
        healthSliderFX.DOKill();
        healthSliderFX.DOValue(evt.CurrentHealth, 0.9f).SetEase(Ease.OutQuad);
        //healthText.text = $"{evt.CurrentHealth} / {evt.MaxHealth}";
    }
}
