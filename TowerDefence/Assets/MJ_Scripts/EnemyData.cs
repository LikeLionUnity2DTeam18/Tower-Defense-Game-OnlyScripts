using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float maxHealth;
    public float attackPower;
    public float moveSpeed;    
    public GameObject enemyPrefab; // 적의 프리팹
    public RuntimeAnimatorController enemyAnim;
}
