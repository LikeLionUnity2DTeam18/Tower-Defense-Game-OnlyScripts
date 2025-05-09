/* 구조체 이름을 이벤트 이름으로 사용합니다
 * 해당 이벤트에서 전달할 값들을 선언해주시면 됩니다. 여러개도 가능
 * 생성자를 정의해주시면 트리거 하기 편해집니다
 * Trigger(new PlayerHealthChanged { PrevHealth = 20, ChangedHealth = 10 });
 * Trigger(new PlayerHealthChanged(20,10);
 */


using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 체력이 변경됐을 때, 
/// 생성자 (변경 전 체력, 변경 후 체력)
/// </summary>
public struct PlayerHealthChanged
{
    public int PrevHealth { get; private set; }
    public int ChangedHealth { get; private set; }

    public PlayerHealthChanged(int prev, int changed)
    {
        PrevHealth = prev;
        ChangedHealth = changed;
    }
}

public struct PlayerFireBreathEnded
{

}

public struct PlayerStatChanged
{
    public PlayerStatTypes type { get; private set; }
    public float value { get; private set; }

    public PlayerStatChanged(PlayerStatTypes type, float value)
    {
        this.type = type;
        this.value = value;
    }
}

public struct PlayerFireBreathStarted
{
    public FireBreathController fireskill;
    public PlayerFireBreathStarted(FireBreathController fireskill)
    {
        this.fireskill = fireskill;
    }
}

public struct PlayerLevelChanged
{
    public int newLevel;
    public PlayerLevelChanged(int newLevel)
    {
        this.newLevel = newLevel;
    }
}

public struct PlayerEquipmentSlotChanged
{
    public List<Sprite> sprites;
    public PlayerEquipmentSlotChanged(List<Sprite> sprites)
    {
        this.sprites = sprites;
    }
}

public struct PlayerInventorySlotChanged
{
    public List<Sprite> sprites;
    public PlayerInventorySlotChanged(List<Sprite> sprites)
    {
        this.sprites = sprites;
    }
}

public struct InventorySlotClicked
{
    public int slotNumber;
    public InventorySlotClicked(int slotNumber)
    {
        this.slotNumber = slotNumber;
    }
}

public struct EquipmentSlotClicked
{
    public EquipmentSlotType slot;
    public EquipmentSlotClicked(EquipmentSlotType slot)
    {
        this.slot = slot;
    }
}

//
public struct InventoryTooltipOnMouse
{
    public bool IsTooltipOn;
    public Vector2 UIposition;
    public int SlotNumber;

    public InventoryTooltipOnMouse(bool isTooltipOn, Vector2 uiposition, int slotNumber)
    {
        this.IsTooltipOn = isTooltipOn;
        this.UIposition = uiposition;
        this.SlotNumber = slotNumber;
    }
}

public struct EquipmentTooltipOnMouse
{
    public bool IsTooltipOn;
    public Vector2 UIPosition;
    public EquipmentSlotType Slot;

    public EquipmentTooltipOnMouse(bool isTooltipOn, Vector2 uiposition, EquipmentSlotType slot)
    {
        this.IsTooltipOn = isTooltipOn;
        this.UIPosition = uiposition;
        this.Slot = slot;
    }
}