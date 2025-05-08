using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// 인벤토리를 관리할 인벤토리매니저
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private List<ItemData> database; // 모든 아이템 db
    [SerializeField] private List<ItemData> startingItems;  // 시작아이템
    [SerializeField] private List<ItemData> possibleItems;  // 생성 가능한 아이템
    [SerializeField] private PlayerStatDisplayNamesSO statDisplayNames; // 스탯타입-UI표시이름 정의

    /// <summary>
    /// 장비한 아이템을 리스트와 딕셔너리 두가지로 동시에 관리
    /// 딕셔너리는 아이템 종류로 빠르게 아이템 존재여부 체크/값 받아오기 가능
    /// 리스트는 순서대로 UI에 표시하거나 갯수를 세거나 하는 작업에 유리해서
    /// 두가지 자료구조 모두 사용
    /// </summary>
    public List<PlayerItem> equipment; // 장비는 4칸 고정으로, enum EquipmentSlots를 인덱스로 원하는 슬롯에 접근
    public Dictionary<ItemData, PlayerItem> equipmentDictionary;

    //인벤토리에 있는 아이템 관리
    public List<PlayerItem> inventory;
    public Dictionary<ItemData, PlayerItem> inventoryDictionary;



    private void Awake()
    {
        // 싱글톤
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        //리스트, 딕셔너리 초기화
        //장비슬롯은 4개짜리 List를 null로 채워서 EquipmentSLots에 대응하는 인덱스로 관리
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
        // 시작장비 착용
        EquipStartingItems();
        // 생성가능 아이템 리스트 초기화
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
    /// 새로운 아이템 데이터의 PlayerItem을 생성 후 인벤토리에 넣고, 아이템 정보를 return
    /// 
    /// </summary>
    /// <param name="data"></param>
    private PlayerItem AddNewItemToInventory(ItemData data)
    {
        PlayerItem newItem = null;
        if(inventoryDictionary.ContainsKey(data)) // 딕셔너리 키에 이미 아이템이 있으면 경고
        {
            Debug.LogWarning("이미 존재하는 아이템 중복 생성");
        }
        else
        {
            newItem = new(data);
            AddExistingItemToInventory(newItem);
        }
        UpdateInventoryUI();

        return newItem;
    }

    /// <summary>
    /// 이미 존재하는 아이템을 인벤토리로 이동시키는 경우
    /// 주로 장비된 아이템 장착 해제
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

    /// <summary>
    /// 안빈토리 아이템 제거, 주로 아이템 장비 후 인벤토리에서 제거
    /// </summary>
    /// <param name="data"></param>
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
    /// 아이템 장착, 같은슬롯에 장착한 아이템이 있을경우 장비 해제 후 인벤토리로 보냄
    /// </summary>
    /// <param name="item"></param>
    private void EquipItem(PlayerItem item)
    {
        RemoveInventoryItem(item.data);

        int index = (int)item.data.ItemSlot; // 해당 장비의 슬롯정보를 enum으로 가지고 있으니, int로 타입캐스팅 해서 인덱스로 사용
        if (equipment[index] != null)
        {
            UnequipItem(equipment[index]);
        }

        equipment[index] = item;
        equipmentDictionary.Add(item.data, item);
        item.AddModifiersOnEquip();  // 아이템 효과를 플레이어 및 스킬 스탯에 적용

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

        item.RemoveModifiersOnUnequip();  // 아이템 효과를 플레이어 및 스킬 스탯에서 제거 

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



        var newItem = AddNewItemToInventory(data);
        possibleItems.RemoveAt(index);

        //새로 추가된 아이템 보여주는 UI 메서드
        // ex) ShowItemTooltip(newItem);
    }

    /// <summary>
    /// 아이템에 스탯타입-표시할문자열이 연결된 데이터파일 전달 후
    /// 조립된 툴팁 string을 받아서 return
    /// </summary>
    /// <param name="item"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    public string GetTooltipText(PlayerItem item, PlayerStatDisplayNamesSO def)
    {
        return item.GetTooltipText(def);
    }
}
