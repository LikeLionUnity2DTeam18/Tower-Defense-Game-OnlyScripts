using System;

public class GoldModel
{
    public int CurrentGold { get; private set; }

    public void AddGold(int amount)
    {
        CurrentGold += amount;
        EventManager.Trigger(new GoldChanged(CurrentGold));
    }

    public void SpendGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
            EventManager.Trigger(new GoldChanged(CurrentGold));
        }
    }

    public void SetGold(int value)
    {
        CurrentGold = value;
        EventManager.Trigger(new GoldChanged(CurrentGold));
    }
}
