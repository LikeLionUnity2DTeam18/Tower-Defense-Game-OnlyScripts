using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float maxHealth;
    public float attackPower;
    public float moveSpeed;    
    public GameObject enemyPrefab; // ���� ������
    public RuntimeAnimatorController enemyAnim;
}
