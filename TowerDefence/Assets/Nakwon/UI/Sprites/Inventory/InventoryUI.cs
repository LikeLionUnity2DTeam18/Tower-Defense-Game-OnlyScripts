using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Sprite slotImage;
    [SerializeField] private List<Image> EquipmentSlotImages;
    [SerializeField] private List<Image> InventorySlotImages;
    private Canvas canvas;


    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        EventManager.AddListener<PlayerEquipmentSlotChanged>(OnEquipItemChanged);
        EventManager.AddListener<PlayerInventorySlotChanged>(OnInventoryItemChanged);
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
}
