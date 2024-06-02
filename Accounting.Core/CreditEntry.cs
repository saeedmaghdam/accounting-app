using System.Diagnostics;

namespace Accounting.Core
{
    [DebuggerDisplay("{DebuggerName}")]
    public record CreditEntry(Account Account, double Amount) : Entry(Account, Amount)
    {
        public static CreditEntry Create(Account account, double amount) => new(account, amount);

        private string DebuggerName => $"{"CreditEntry",-15}{Account.Name,-35} {Amount,12:N2}";
    }
}
