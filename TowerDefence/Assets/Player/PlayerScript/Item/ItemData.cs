
using UnityEngine;

/// <summary>
/// 아이템 데이터를 저장하는 파일
/// </summary>
public enum EquipmentSlotType { Head, Body, Weapon, Amulet}

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Player/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("아이템 기본 정보")]
    public string ItemName;
    public EquipmentSlotType ItemSlot;
    public Sprite ItemIcon;

    [Header("플레이어 스탯 관련")]
    public float AttackDamagePercent;
    public float AttackSpeedPercent; // 초당 공격 횟수
    public float SkillPower;
    public float MoveSpeed;

    [Header("BindShot 관련")]
    public float BindShotBindTime;

    [Header("Wall 관련")]
    public float WallMaxHP;

    [Header("PowerUp 관련")]
    public float PowerupDamageUpAmount;

    [Header("Fire Breath 관련")]
    public float FireBreathDamage;
    public float FireBreathDamageInterval;

}
