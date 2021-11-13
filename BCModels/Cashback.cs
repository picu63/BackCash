namespace BC.Models;

public class Cashback
{
    public decimal Amount { get; set; }
    public CashbackType Type { get; set; }
}

public enum CashbackType
{
    Cash,
    Percentage
}