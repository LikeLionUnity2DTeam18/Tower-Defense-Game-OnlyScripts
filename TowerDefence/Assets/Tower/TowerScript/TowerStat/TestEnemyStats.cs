using UnityEngine;

public class TestEnemyStats : TowerStats
{
    protected override void Die()
    {
        Destroy(gameObject);
    }
}
