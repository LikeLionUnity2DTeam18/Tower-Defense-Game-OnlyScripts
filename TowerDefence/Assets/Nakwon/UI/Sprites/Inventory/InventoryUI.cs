using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Sprite slotImage;
    [SerializeField] private List<Image> EquipmentSlotImages;
    [SerializeField] private List<Image> InventorySlotImages;
    [SerializeField] private InventoryTooltip tooltip;
    private Canvas canvas;


    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        EventManager.AddListener<PlayerEquipmentSlotChanged>(OnEquipItemChanged);
        EventManager.AddListener<PlayerInventorySlotChanged>(OnInventoryItemChanged);
        EventManager.AddListener<InventoryTooltipOnMouse>(OnInventoryTooltipOnMouse);
        EventManager.AddListener<EquipmentTooltipOnMouse>(OnEquipmentTooltipOnMouse);
        EventManager.AddListener<ToggleInventory>(OnToggleInventory);
    }

    void Start()
    {
        canvas.enabled = false;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerEquipmentSlotChanged>(OnEquipItemChanged);
        EventManager.RemoveListener<PlayerInventorySlotChanged>(OnInventoryItemChanged);
        EventManager.RemoveListener<InventoryTooltipOnMouse>(OnInventoryTooltipOnMouse);
        EventManager.RemoveListener<EquipmentTooltipOnMouse>(OnEquipmentTooltipOnMouse);
        EventManager.RemoveListener<ToggleInventory>(OnToggleInventory);
    }

    private void OnEquipItemChanged(PlayerEquipmentSlotChanged evt)
    {
        int count = 0;
        foreach (var sprite in evt.sprites)
        {
            if (sprite == null)
            {
                EquipmentSlotImages[count].sprite = slotImage;
            }
            else
            {
                EquipmentSlotImages[count].sprite = sprite;
            }
            count++;
        }
    }

    private void OnInventoryItemChanged(PlayerInventorySlotChanged evt)
    {
        int count = 0;
        foreach (var sprite in evt.sprites)
        {
            if (sprite == null)
            {
                InventorySlotImages[count].sprite = slotImage;
            }
            else
            {
                InventorySlotImages[count].sprite = sprite;
            }
            count++;
        }
    }

    private void OnToggleInventory(ToggleInventory _)
    {
        canvas.enabled = !canvas.enabled;
    }

    private void OnInventoryTooltipOnMouse(InventoryTooltipOnMouse evt)
    {
        if (evt.IsTooltipOn)
        {
            string text = InventoryManager.Instance.GetInventoryTooltipText(evt.SlotNumber);
            if (text == null)
                return;
            Vector2 pos = evt.UIposition + new Vector2(280, -150);
            tooltip.ShowTooltip(pos, text);
        }
        else
        {
            tooltip.HideTooltip();
        }
    }

    private void  OnEquipmentTooltipOnMouse(EquipmentTooltipOnMouse evt)
    {
        if(evt.IsTooltipOn)
        {
            string text = InventoryManager.Instance.GetEquipmentTooltipText(evt.Slot);
            if (text == null)
                return;
            Vector2 pos = evt.UIPosition + new Vector2(280, -150);
            tooltip.ShowTooltip(pos, text);
        }
        else
        {
            tooltip.HideTooltip();            
        }
    }
}
