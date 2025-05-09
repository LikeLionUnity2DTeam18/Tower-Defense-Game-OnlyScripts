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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.AddListener<PlayerEquipmentSlotChanged>(OnEquipItemChanged);
        EventManager.AddListener<PlayerInventorySlotChanged>(OnInventoryItemChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<PlayerEquipmentSlotChanged>(OnEquipItemChanged);
        EventManager.RemoveListener<PlayerInventorySlotChanged>(OnInventoryItemChanged);
    }

    private void OnEquipItemChanged(PlayerEquipmentSlotChanged evt)
    {
        int count = 0;
        foreach(var sprite in evt.sprites)
        {
            if(sprite == null)
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
        foreach(var sprite in evt.sprites)
        {
            if(sprite == null)
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


}
