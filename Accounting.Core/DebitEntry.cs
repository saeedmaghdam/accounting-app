
using System.Diagnostics;

namespace Accounting.Core
{
    [DebuggerDisplay("{DebuggerName}")]
    public record DebitEntry(Account Account, double Amount) : Entry(Account, Amount)
    {
        public static DebitEntry Create(Account account, double amount) => new(account, amount);

        private string DebuggerName => $"{"DebitEntry",-15}{Account.Name,-35} {Amount,12:N2}";
    }
}
