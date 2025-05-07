using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] private List<ItemData> database; // 모든 아이템 db
    [SerializeField] private List<ItemData> startingItems;  // 시작아이템
    [SerializeField] private List<ItemData> possibleItems;  // 생성 가능한 아이템
    [SerializeField] private PlayerStatDisplayNamesSO statDisplayNames; // 스탯타입-UI표시이름 정의

    public List<PlayerItem> equipment;
    public Dictionary<ItemData, PlayerItem> equipmentDictionary;

    public List<PlayerItem> inventory;
    public Dictionary<ItemData, PlayerItem> inventoryDictionary;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        int slotCount = System.Enum.GetValues(typeof(EquipmentSlots)).Length;
        equipment = new List<PlayerItem>();
        for (int i = 0; i < slotCount; i++)
            equipment.Add(null);

        equipmentDictionary = new();
        inventory = new();
        inventoryDictionary = new();
    }

    private void Start()
    {

        EquipStartingItems();

        InitilizePossibleItemList();
    }

    /// <summary>
    /// DB에서 시작아이템을 제외한 아이템을 추가가능아이템 리스트로 초기화
    /// </summary>
    private void InitilizePossibleItemList()
    {
        possibleItems = new List<ItemData>(database);
        possibleItems.RemoveAll(item => startingItems.Contains(item));
    }

    /// <summary>
    /// 시작아이템 장착한 상태로 시작
    /// </summary>
    private void EquipStartingItems()
    {
        foreach(var data in startingItems)
        {
            var newItem = new PlayerItem(data);
            EquipItem(newItem);
        }
    }

    /// <summary>
    /// 새로운 아이템이 인벤토리에 추가되는 경우
    /// </summary>
    /// <param name="data"></param>
    private void AddNewItemToInventory(ItemData data)
    {
        if(inventoryDictionary.ContainsKey(data))
        {
            Debug.LogWarning("이미 존재하는 아이템 중복 생성");
        }
        else
        {
            PlayerItem newItem = new(data);
            AddExistingItemToInventory(newItem);
        }
        UpdateInventoryUI();
    }

    /// <summary>
    /// 이미 존재하는 아이템을 인벤토리로 이동시키는 경우
    /// </summary>
    /// <param name="item"></param>
    private void AddExistingItemToInventory(PlayerItem item)
    {
        if(inventoryDictionary.ContainsKey(item.data))
        {
            Debug.LogWarning("이미 존재하는 아이템 중복 생성");
        }
        else
        {
            inventory.Add(item);
            inventoryDictionary.Add(item.data, item);
        }
        UpdateInventoryUI();
    }

    private void RemoveInventoryItem(ItemData data)
    {
        if (inventoryDictionary.TryGetValue(data, out PlayerItem itemToRemove))
        {
            inventory.Remove(itemToRemove);
            inventoryDictionary.Remove(data);
        }
        UpdateInventoryUI();
    }
    /// <summary>
    /// 아이템 장착, 같은슬롯에 장착한 아이템이 있을경우 인벤토리로 보냄
    /// </summary>
    /// <param name="item"></param>
    private void EquipItem(PlayerItem item)
    {
        RemoveInventoryItem(item.data);

        int index = (int)item.data.ItemSlot;
        if (equipment[index] != null)
        {
            UnequipItem(equipment[index]);
        }

        equipment[index] = item;
        equipmentDictionary.Add(item.data, item);
        item.AddModifiersOnEquip();

        UpdateEquipmentUI();
    }

    /// <summary>
    /// 아이템 장비 해제 후 인벤토리에 추가
    /// </summary>
    /// <param name="item"></param>
    private void UnequipItem(PlayerItem item)
    {
        int index = (int)item.data.ItemSlot;
        equipment[index] = null;
        equipmentDictionary.Remove(item.data);

        item.RemoveModifiersOnUnequip();

        AddExistingItemToInventory(item);
    }
    /// <summary>
    /// 인벤토리 및 장비창 UI 업데이트
    /// </summary>
    public void UpdateItemUI()
    {
        UpdateInventoryUI();
        UpdateEquipmentUI();
    }

    /// <summary>
    /// 인벤토리 UI 업데이트
    /// </summary>
    private void UpdateInventoryUI()
    {

    }

    /// <summary>
    /// 장비창 UI 업데이트
    /// </summary>
    private void UpdateEquipmentUI()
    {

    }

    /// <summary>
    /// 생성가능한 아이템중 하나 추가 
    /// </summary>
    private void AddRandomPossibleItemToInventory()
    {
        if (possibleItems.Count <= 0)
        {
            Debug.LogWarning("추가 가능한 아이템 없음");
            return;
        }
        int index = Random.Range(0,possibleItems.Count);
        var data = possibleItems[index];

        //새로 추가된 아이템 보여주는 UI 메서드

        AddNewItemToInventory(data);
        possibleItems.RemoveAt(index);
    }

    public string GetTooltipText(PlayerItem item, PlayerStatDisplayNamesSO def)
    {
        return item.GetTooltipText(def);
    }
}
