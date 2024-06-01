namespace Accounting.Core
{
    public record CreditEntry(Account Account, double Amount) : Entry(Account, Amount)
    {
        public static CreditEntry Create(Account account, double amount) => new(account, amount);
    }
}
