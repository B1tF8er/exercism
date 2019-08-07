using System;

public class BankAccount
{
    private const string GetBalanceError = "Cannot get balance on an account that isn't open";
    private const string UpdateBalanceError = "Cannot update balance on an account that isn't open";
    private readonly object padlock;
    private decimal balance;
    private bool isOpen;

    public BankAccount() => padlock = new object();

    public void Open() => isOpen = true;

    public void Close() => isOpen = false;

    public decimal Balance
    {
        get
        {
            GuardOpenAccount(GetBalanceError);

            lock (padlock)
                return balance;
        }
    }

    public void UpdateBalance(decimal change)
    {
        GuardOpenAccount(UpdateBalanceError);

        lock (padlock)
            balance += change;
    }

    private void GuardOpenAccount(string error)
    {
        if (!isOpen)
            throw new InvalidOperationException(error);
    }
}
