using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private int slotNumber;
    private Button button;
    private EventTrigger eventTrigger;
    private void Awake()
    {
        button = GetComponent<Button>();
        eventTrigger = GetComponent<EventTrigger>();
        button.onClick.AddListener(() => OnClick());

        RegisterEventTrigger();
    }

    /// <summary>
    /// 이벤트트리거에 마우스 엔터/엑시트 등록
    /// </summary>
    private void RegisterEventTrigger()
    {
        var enterEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        enterEntry.callback.AddListener((data) => OnPointerEnter());
        eventTrigger.triggers.Add(enterEntry);

        var exitEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        exitEntry.callback.AddListener((data) => OnPointerExit());
        eventTrigger.triggers.Add(exitEntry);
    }

    public void OnClick()
    {
        EventManager.Trigger<InventorySlotClicked>(new InventorySlotClicked(slotNumber));
    }

    public void OnPointerEnter()
    {
        Debug.Log("mouseEnter");
        EventManager.Trigger<InventoryTooltipOnMouse>(new InventoryTooltipOnMouse(true,transform.position,slotNumber));
    }

    public void OnPointerExit()
    {
        EventManager.Trigger<InventoryTooltipOnMouse>(new InventoryTooltipOnMouse(false, transform.position, slotNumber));
    }
}
