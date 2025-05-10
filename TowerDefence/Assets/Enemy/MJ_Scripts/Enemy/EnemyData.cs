using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("기본 정보")]
    public EnemyType enemyType;
    public string enemyName;
    public RuntimeAnimatorController enemyAnim;
    public GameObject enemyPrefab;

    [Header("능력치")]
    public float maxHealth;
    public float attackPower;
    public float moveSpeed;
    public float targetRange;         // 공격 범위
    public float detectTowerRange;   // 감지 범위 (추천: 공격 범위의 1.3~2x)

    [Header("골드 량")]
    public int rewardGold = 10;

    [Header("자폭 전용")]
    public float explosionRadius;     // Boomer 전용 폭발 범위

    [Header("공격 이펙트")]
    public GameObject attackEffectPrefab;
    public GameObject attackEffectFront;
    public GameObject attackEffectBack;

    [Header("피격 이펙트")]
    public GameObject hitEffectPrefab;  // 단일형 (Archer, BountyHunter)
    public GameObject hitEffectFront;   // Ghost 전용
    public GameObject hitEffectBack;

    #region 원본 값 저장
    [HideInInspector] public float baseMaxHealth;
    [HideInInspector] public float baseAttackPower;
    [HideInInspector] public float baseMoveSpeed;

    public void CacheBaseValues()
    {
        baseMaxHealth = maxHealth;
        baseAttackPower = attackPower;
        baseMoveSpeed = moveSpeed;
    }

    public void ResetToBaseValues()
    {
        maxHealth = baseMaxHealth;
        attackPower = baseAttackPower;
        moveSpeed = baseMoveSpeed;
    }
    #endregion

    public EnemyData Clone()
    {
        EnemyData clone = ScriptableObject.CreateInstance<EnemyData>();
        clone.enemyType = this.enemyType;
        clone.enemyName = this.enemyName;
        clone.enemyAnim = this.enemyAnim;
        clone.enemyPrefab = this.enemyPrefab;

        clone.maxHealth = this.maxHealth;
        clone.attackPower = this.attackPower;
        clone.moveSpeed = this.moveSpeed;
        clone.targetRange = this.targetRange;
        clone.detectTowerRange = this.detectTowerRange;
        clone.rewardGold = this.rewardGold;
        clone.explosionRadius = this.explosionRadius;

        clone.attackEffectPrefab = this.attackEffectPrefab;
        clone.attackEffectFront = this.attackEffectFront;
        clone.attackEffectBack = this.attackEffectBack;
        clone.hitEffectPrefab = this.hitEffectPrefab;
        clone.hitEffectFront = this.hitEffectFront;
        clone.hitEffectBack = this.hitEffectBack;

        clone.CacheBaseValues();

        return clone;
    }

}
