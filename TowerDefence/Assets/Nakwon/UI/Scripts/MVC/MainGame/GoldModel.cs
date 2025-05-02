using System;

public class GoldModel
{
    public int CurrentGold { get; private set; }

    public void AddGold(int amount)
    {
        CurrentGold += amount;
        NotifyChange();
    }

    public void SpendGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
            NotifyChange();
        }
    }

    public void SetGold(int value)
    {
        CurrentGold = value;
        NotifyChange();
    }

    private void NotifyChange()
    {
        EventManager.Trigger(new GoldChanged(CurrentGold));
    }
}
