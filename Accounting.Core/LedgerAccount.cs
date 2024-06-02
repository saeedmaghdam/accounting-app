using System.Diagnostics;

namespace Accounting.Core
{
    [DebuggerDisplay("{DebuggerName}")]
    public class LedgerAccount
    {
        public Account Account { get; }
        public double Balance { get; private set; }
        public List<Entry> Entries { get; }

        public LedgerAccount(Account account)
        {
            Account = account;
            Balance = default;
            Entries = new();
        }

        public static LedgerAccount Create(Account account) => new(account);

        internal void AddDebitEntry(DebitEntry debitEntry)
        {
            Balance += debitEntry.Amount;
            Entries.Add(debitEntry);
        }

        internal void AddCreditEntry(CreditEntry creditEntry)
        {
            Balance -= creditEntry.Amount;
            Entries.Add(creditEntry);
        }

        public override string ToString()
        {
            return $"{Account} {Balance}";
        }

        private string DebuggerName
        {
            get {
                var balance = Balance > 0 ? "+" + Balance.ToString() : Balance.ToString();
                return $"{Account.AccountType,-12}{Account.Name,-35}{balance}";
            }
        }
    }
}
