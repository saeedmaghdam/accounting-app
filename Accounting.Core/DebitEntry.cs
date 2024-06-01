
namespace Accounting.Core
{
    public record DebitEntry(Account Account, double Amount) : Entry(Account, Amount)
    {
        public static DebitEntry Create(Account account, double amount) => new(account, amount);
    }
}
