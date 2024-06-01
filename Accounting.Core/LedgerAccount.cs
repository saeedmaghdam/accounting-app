namespace Accounting.Core
{
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
    }
}
