using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public EnemyType enemyType; // 적의 타입

    public string enemyName;
    public float maxHealth;
    public float attackPower;
    public float moveSpeed;
    public float targetRange;

    public float explosionRadius; //폭발 범위 boomer만 해당

    public GameObject enemyPrefab; // 적의 프리팹
    
    public GameObject attackEffectPrefab;
    public GameObject attackEffectFront; // 공격 이펙트가 방향에 따라 다른 경우 사용
    public GameObject attackEffectBack;

    public RuntimeAnimatorController enemyAnim;
}
