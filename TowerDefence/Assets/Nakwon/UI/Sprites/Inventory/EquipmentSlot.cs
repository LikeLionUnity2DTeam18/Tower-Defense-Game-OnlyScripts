using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private EquipmentSlotType slot;
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClick());
    }

    public void OnClick()
    {
        EventManager.Trigger<EquipmentSlotClicked>(new EquipmentSlotClicked(slot));
    }
}
