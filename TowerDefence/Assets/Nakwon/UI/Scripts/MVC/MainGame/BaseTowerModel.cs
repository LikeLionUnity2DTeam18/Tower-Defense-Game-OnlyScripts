using UnityEngine;

public class BaseTowerModel

{
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    public BaseTowerModel(int _MaxHealth)
    {
        MaxHealth = _MaxHealth;
        CurrentHealth = MaxHealth;

        NotifyChange();
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        NotifyChange();
    }

    private void NotifyChange()
    {
        EventManager.Trigger(new BaseTowerHealthChanged(CurrentHealth, MaxHealth));
    }

    public bool IsDead() => CurrentHealth <= 0;
}
